using System;
using RoutingServer.ServiceReference1;
using System.Diagnostics;
using System.Device.Location;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Caching;
using RoutingServer.RestClients.OpenStreetMap.Map;
using System.Threading;
using RoutingServer.Contracts;
using ActiveMQTest;
using System.Text.Json;
using RoutingServer.RestClients.OpenStreetMap.Nominatim;

namespace RoutingServer
{
    public class RoutingService : IRoutingService
    {

        
        OpenStreetMapGeoCodeClient openStreetMapGeoCodeClient = OpenStreetMapGeoCodeClient.GetClient("5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946", new Uri("https://api.openrouteservice.org/geocode/"));
        OpenStreetMapDirectionsClient openStreetMapDirectionsClient = OpenStreetMapDirectionsClient.GetClient("5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946", new Uri("https://api.openrouteservice.org/v2/directions/"));
        OpenStreetMapNomatimClient openStreetMapNomatimClient = OpenStreetMapNomatimClient.GetClient("5b3ce3597851110001cf6248fd78269d24c847af8598c68db3e67946", new Uri("https://nominatim.openstreetmap.org/"));

        RoutingHelper routingHelper;
        RoutingHandler routingHandler;
        
        public RoutingService()
        {
            routingHelper = new RoutingHelper(openStreetMapDirectionsClient, openStreetMapGeoCodeClient);
            routingHandler = new RoutingHandler(openStreetMapGeoCodeClient, openStreetMapNomatimClient, routingHelper);
        }

        public Itinerary GetItinerary(string origin, string destination)
        {
            return routingHandler.HandleItineraryRequest(origin, destination);
        }

        public Itinerary GetItineraryV2(string origin, string destination, string queuename)
        {
            return routingHandler.HandleItineraryRequestV2(origin, destination,queuename);
            
        }

        public QueueWayPoints UpdateSteps(GeoCoordinate origin, GeoCoordinate destination, string currentMode, string queue)
        {
            return routingHandler.HandleStepUpdateRequest(origin, destination, currentMode, queue);
        }
    }
}
