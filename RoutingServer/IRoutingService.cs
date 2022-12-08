using RoutingServer.Contracts;
using RoutingServer.RestClients.OpenStreetMap.Map;
using System.Device.Location;
using System.ServiceModel;


namespace RoutingServer
{
    [ServiceContract]
    public interface IRoutingService
    {

        [OperationContract]
        Itinerary GetItinerary(string origin, string destination);

        [OperationContract]
        QueueWayPoints UpdateSteps(GeoCoordinate origin, GeoCoordinate destination, string currentMode, string queue);

        [OperationContract]
        Itinerary GetItineraryV2(string origin, string destination, string queuename);
    }

}
