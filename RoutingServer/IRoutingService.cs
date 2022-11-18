using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RoutingServer
{
    [ServiceContract]
    public interface IRoutingService
    {
        [OperationContract]
        Itinerary GetItinerary(GeoCoordinates origin, GeoCoordinates destination);

    }

    

}
