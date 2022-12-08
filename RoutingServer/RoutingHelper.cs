using ActiveMQTest;
using RoutingServer.Contracts;
using RoutingServer.RestClients.OpenStreetMap.Map;
using RoutingServer.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Text.Json;
using System.Threading;
using Station = RoutingServer.ServiceReference1.Station;

namespace RoutingServer
{
    public class RoutingHelper
    {
        private const int STATIONS_CACHE_COUNT = 6;

        private OpenStreetMapDirectionsClient openStreetMapDirectionsClient;
        private OpenStreetMapGeoCodeClient openStreetMapGeoCodeClient;
        private ProxyServiceClient proxyService;
        private ObjectCache cache = MemoryCache.Default;


        public RoutingHelper(OpenStreetMapDirectionsClient openStreetMapDirectionsClient, OpenStreetMapGeoCodeClient openStreetMapGeoCodeClient)
        {
            this.openStreetMapDirectionsClient = openStreetMapDirectionsClient;
            this.openStreetMapGeoCodeClient = openStreetMapGeoCodeClient;
            proxyService = new ProxyServiceClient();
        }

        private double ComputeDistanceToStation(GeoCoordinate location, Station station)
        {
            GeoCoordinate stationGeoCoordinates = new GeoCoordinate()
            {
                Latitude = station.position.lat,
                Longitude = station.position.lng
            };
            return location.GetDistanceTo(stationGeoCoordinates);
        }

        private Itinerary GetItineraryToStationByFoot(GeoCoordinate location, Station station)
        {
            GeoCoordinate stationGeoCoordinates = new GeoCoordinate()
            {
                Latitude = station.position.lat,
                Longitude = station.position.lng
            };
            return openStreetMapDirectionsClient.GetItineraryByFoot(location, stationGeoCoordinates).GetAwaiter().GetResult();
        }

        private Itinerary GetItineraryBetweenStationsByBicycle(Station originStation, Station destinationStation)
        {
            GeoCoordinate originStationGeoCoordinates = new GeoCoordinate()
            {
                Latitude = originStation.position.lat,
                Longitude = originStation.position.lng
            };
            GeoCoordinate destinationStationGeoCoordinates = new GeoCoordinate()
            {
                Latitude = destinationStation.position.lat,
                Longitude = destinationStation.position.lng
            };
            return openStreetMapDirectionsClient.GetItineraryByBicycle(originStationGeoCoordinates, destinationStationGeoCoordinates).GetAwaiter().GetResult();
        }

        private Station[] NearestStations(GeoCode location)
        {
            string localityContract = location.GetLocality();
            string macroCountyContract = location.GetMacroCounty();


            if (string.IsNullOrEmpty(localityContract) || string.IsNullOrEmpty(macroCountyContract))
            {
                throw new Exception("No locality or macro county found for the given location");
            }

            Station[] stationsByMacroCounty = proxyService.GetStationsByContract(macroCountyContract);

            Station[] stationsByLocality = proxyService.GetStationsByContract(localityContract);

            return stationsByLocality.Concat(stationsByMacroCounty).ToArray();
        }

        public Itinerary GetItinerary(GeoCode origin, GeoCode destination)
        {
            GeoCoordinate originCoordinates = origin.GetGeoCoordinate();
            GeoCoordinate destinationCoordinates = destination.GetGeoCoordinate();
            Itinerary byFoot = new Itinerary();
            try
            {
                byFoot = openStreetMapDirectionsClient.GetItineraryByFoot(originCoordinates, destinationCoordinates).Result;
                Itinerary byBicycle = GetItineraryByBicycle(origin, destination);

                Debug.WriteLine(byFoot.routes[0].summary.duration + "vs" + byBicycle.routes[0].summary.duration);
                if (byFoot.routes[0].summary.duration > byBicycle.routes[0].summary.duration)
                {
                    byBicycle.message = "It is better to go by bicycle";
                    byBicycle.mode = "Bicycle";
                    byBicycle.bbox[2] = destinationCoordinates.Longitude;
                    byBicycle.bbox[3] = destinationCoordinates.Latitude;
                    return byBicycle;
                }
                else
                {
                    byFoot.message = "It is better to go by foot";
                    byFoot.mode = "Foot";
                    byFoot.bbox[2] = destinationCoordinates.Longitude;
                    byFoot.bbox[3] = destinationCoordinates.Latitude;
                    return byFoot;
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                byFoot.message = e.Message;
                byFoot.mode = "Foot";
                byFoot.bbox[2] = destinationCoordinates.Longitude;
                byFoot.bbox[3] = destinationCoordinates.Latitude;
                return byFoot;
            }
        }


        public Itinerary GetItineraryV2(GeoCode origin, GeoCode destination, string queuename)
        {
            Itinerary itinerary = GetItinerary(origin, destination);
            Step[] stepsLeft = itinerary.GetSteps();
            string queueMessage = JsonSerializer.Serialize<Step>(stepsLeft[0]);
            cache.Add("bicycle_steps" + queuename, stepsLeft,DateTimeOffset.Now.AddSeconds(20));
            cache.Add("bicycle_itinerary" + queuename, itinerary, DateTimeOffset.Now.AddSeconds(20));
            StepsPublisher.SendNewMessage(queueMessage, queuename);
            itinerary.routes[0].segments = null;
            return itinerary;
        }



        private KeyValuePair<Station, Itinerary> NearestStationItinerary(GeoCode location, bool forPickUp = false, bool forDrop = false, Station[] ignoredstations = null)
        {

            List<KeyValuePair<Station, Itinerary>> list = new List<KeyValuePair<Station, Itinerary>>();
            GeoCoordinate locationGeoCoordinates = location.GetGeoCoordinate();
            Station[] stations = NearestStations(location);
            if (stations.Length == 0) throw new Exception("No station found");
            if (ignoredstations != null)
            {
                foreach (Station ignoredstation in ignoredstations)
                {
                    stations = stations.ToList().Where(st => (st.position.lat != ignoredstation.position.lat && st.position.lng != ignoredstation.position.lng)).ToArray();
                }
            }
            
            stations = stations.Where(st => !forPickUp ? !forDrop ? false : st.available_bike_stands > 0 : st.available_bikes > 0).
                        OrderBy(st => ComputeDistanceToStation(locationGeoCoordinates, st)).
                        Take(STATIONS_CACHE_COUNT).ToArray();


            stations.ToList().ForEach(st => list.Add(new KeyValuePair<Station, Itinerary>(st, GetItineraryToStationByFoot(locationGeoCoordinates, st))));

            list = list.OrderBy(kvp => kvp.Value.GetDuration()).ToList();

            return list.FirstOrDefault();

        }

        public Itinerary GetItineraryByBicycle(GeoCode origin, GeoCode destination)
        {

            KeyValuePair<Station, Itinerary> fromOriginToStation = NearestStationItinerary(origin, true);
            Thread.Sleep(2000);
            KeyValuePair<Station, Itinerary> fromStationToDestination = NearestStationItinerary(destination, false, true, new Station[] { fromOriginToStation.Key });

            Itinerary fromStationToSation = GetItineraryBetweenStationsByBicycle(fromOriginToStation.Key, fromStationToDestination.Key);

            IEnumerable<GeoCoordinate> polyline_one = GooglePolylineConverter.Decode(fromOriginToStation.Value.GetGeometry());
            IEnumerable<GeoCoordinate> polyline_two = GooglePolylineConverter.Decode(fromStationToSation.GetGeometry());
            IEnumerable<GeoCoordinate> polyline_three = GooglePolylineConverter.Decode(fromStationToDestination.Value.GetGeometry());

            IEnumerable<GeoCoordinate> polyline = polyline_one.Concat(polyline_two.Concat(polyline_three.Reverse()));

            string geometry = GooglePolylineConverter.Encode(polyline);
            Segment[] segments = {
                                fromOriginToStation.Value.routes[0].segments[0],
                                fromStationToSation.routes[0].segments[0],
                                fromStationToDestination.Value.routes[0].segments[0]
                            };
            Summary summary = new Summary()
            {
                distance = fromOriginToStation.Value.GetDistance() +
                                            fromStationToSation.GetDistance() +
                                            fromStationToDestination.Value.GetDistance(),
                duration = fromOriginToStation.Value.GetDuration() +
                                            fromStationToSation.GetDuration() +
                                            fromStationToDestination.Value.GetDuration()
            };

            return new Itinerary()
            {
                bbox = new double[] {
                    fromOriginToStation.Value.bbox[0],
                    fromOriginToStation.Value.bbox[1],
                    fromStationToDestination.Value.bbox[2],
                    fromStationToDestination.Value.bbox[3]
                },
                routes = new Route[]
                {
                    new Route()
                    {
                            geometry = geometry,
                            segments = segments,
                            summary = summary

                    }
                }
            };

        }

        public QueueWayPoints GetNextStepsByFoot(GeoCoordinate origin, GeoCoordinate destination, string queue)
        {
            Itinerary itByF = openStreetMapDirectionsClient.GetItineraryByFoot(origin, destination).Result;
            Step[] stepsLeft = itByF.GetSteps();
            string queueMessage = "";
            int i = 0;
            if (stepsLeft.Length != 0)
            {
                while ((stepsLeft[i].maneuver.location[0] == origin.Longitude && stepsLeft[i].maneuver.location[1] == origin.Latitude) && i < stepsLeft.Length)
                {
                    i++;
                }
                queueMessage = JsonSerializer.Serialize<Step>(stepsLeft[i]);
            }
            else
            {
                queueMessage = JsonSerializer.Serialize<Step>(new Step()
                {
                    instruction = "You have arrived at your destination",
                });
            }
            StepsPublisher.SendNewMessage(queueMessage, queue);
            return new QueueWayPoints()
            {
                queuename = queue,
                currentMode = "Foot",
                geometry = itByF.routes[0].geometry
            };
        }

        public QueueWayPoints GetNextStepsByBicycle(GeoCoordinate origin, GeoCoordinate destination, string queue)
        {
            Step[] stepsLeft = (Step[])cache.Get("bicycle_steps" + queue);
            Itinerary itinerary = (Itinerary)cache.Get("bicycle_itinerary" + queue);
            
            string queueMessage = "";
            int i = 0;
            if (stepsLeft.Length != 0)
            {
                Debug.WriteLine("im here too" + stepsLeft.Length);
                while (
                    (stepsLeft[i].maneuver.location == null) ||
                    (stepsLeft[i].maneuver.location[0] == origin.Longitude && stepsLeft[i].maneuver.location[1] == origin.Latitude)
                    && i < stepsLeft.Length)
                {
                    i++;
                }
                queueMessage = JsonSerializer.Serialize<Step>(stepsLeft[i]);
                stepsLeft = stepsLeft.Skip(i).ToArray();
                cache.Remove("bicycle_steps" + queue);
                cache.Add("bicycle_steps"+ queue, stepsLeft, DateTimeOffset.MaxValue);

                Debug.WriteLine("im here" + stepsLeft.Length);

            }
            else
            {
                queueMessage = JsonSerializer.Serialize<Step>(new Step()
                {
                    instruction = "You have arrived at your destination",
                });
            }
            StepsPublisher.SendNewMessage(queueMessage, queue);
            return new QueueWayPoints()
            {
                queuename = queue,
                currentMode = "Bicycle",
                geometry = itinerary.routes[0].geometry
            };
        }

        public GeoCoordinate StationCoordinates(Station station)
        {
            return new GeoCoordinate()
            {
                Latitude = station.position.lat,
                Longitude = station.position.lng
            };
        }


    }
}
