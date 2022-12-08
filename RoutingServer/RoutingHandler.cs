using Apache.NMS;
using RoutingServer.Contracts;
using RoutingServer.RestClients.OpenStreetMap.Map;
using RoutingServer.RestClients.OpenStreetMap.Nominatim;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    public class RoutingHandler
    {
        private OpenStreetMapGeoCodeClient openStreetMapGeoCodeClient;
        private OpenStreetMapNomatimClient openStreetMapNomatimClient;
        private RoutingHelper routingHelper;

        public RoutingHandler(OpenStreetMapGeoCodeClient openStreetMapGeoCodeClient, OpenStreetMapNomatimClient openStreetMapNomatimClient, RoutingHelper routingHelper)
        {
            this.openStreetMapGeoCodeClient = openStreetMapGeoCodeClient;
            this.openStreetMapNomatimClient = openStreetMapNomatimClient;
            this.routingHelper = routingHelper;
        }

        public Itinerary HandleItineraryRequest(string origin, string destination)
        {
            try
            {
                GeoCode originCoordinates = openStreetMapGeoCodeClient.GetAddress(origin).Result;
                GeoCode destinationCoordinates = openStreetMapGeoCodeClient.GetAddress(destination).Result;

                if (originCoordinates.features == null || destinationCoordinates.features == null ||
                    originCoordinates.features.Length == 0 || destinationCoordinates.features.Length == 0)
                {
                    ReRouteContractRequest(originCoordinates, origin);
                    ReRouteContractRequest(destinationCoordinates, destination);
                }


                Itinerary it = routingHelper.GetItinerary(originCoordinates, destinationCoordinates);

                it.found = true;
                return it;
            }
            catch (Exception ex)
            {
                Itinerary itinerary = new Itinerary();
                itinerary.found = false;
                itinerary.message = "Error : " + ex.Message;
                return itinerary;
            }
        }

        public Itinerary HandleItineraryRequestV2(string origin, string destination, string queuename)
        {
            try
            {
                GeoCode originCoordinates = openStreetMapGeoCodeClient.GetAddress(origin).Result;
                GeoCode destinationCoordinates = openStreetMapGeoCodeClient.GetAddress(destination).Result;

                if (originCoordinates.features == null || destinationCoordinates.features == null ||
                    originCoordinates.features.Length == 0 || destinationCoordinates.features.Length == 0)
                {

                    ReRouteContractRequest(originCoordinates, origin);
                    ReRouteContractRequest(destinationCoordinates, destination);
                }

                Itinerary it = routingHelper.GetItineraryV2(originCoordinates, destinationCoordinates, queuename);
                it.found = true;
                return it;
            }
            catch (Exception ex)
            {
                Itinerary itinerary = new Itinerary();
                itinerary.found = false;
                itinerary.message = "Error : " + ex.Message;
                return itinerary;
            }
        }

        public QueueWayPoints HandleStepUpdateRequest(GeoCoordinate origin, GeoCoordinate destination, string currentMode, string queue)
        {
            try
            {
                if (currentMode.Equals("Foot"))
                {
                    QueueWayPoints q = routingHelper.GetNextStepsByFoot(origin, destination, queue);
                    q.found = true;
                    return q;

                }
                else
                {
                    QueueWayPoints q = routingHelper.GetNextStepsByBicycle(origin, destination, queue);
                    q.found = true;
                    return q;
                }

            }
            catch (Exception ex)
            {
                return new QueueWayPoints()
                {
                    queuename = queue,
                    found = false,
                    message = "Error : " + ex.Message,
                };
            }

        }

        private void ReRouteContractRequest(GeoCode coordinates, string address)
        {
            Place plcOg = openStreetMapNomatimClient.GetGeoCode(address).Result;
            if (plcOg == null) throw new Exception("No address found");
            coordinates.features = new Feature[]{
                        new Feature(){
                            geometry = plcOg.GetGeometry(),
                            properties = new Properties(){
                                locality = plcOg.GetLocality(),
                                macrocounty = plcOg.GetMacroCounty()
                            }
                        }
             };
        }
    }
}
