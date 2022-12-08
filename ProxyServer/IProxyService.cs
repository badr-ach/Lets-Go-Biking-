using ProxyServer.JCDECAUXRestClient;
using System.Collections.Generic;
using System.ServiceModel;

namespace ProxyServer
{
    [ServiceContract]
    public interface IProxyService
    {

        [OperationContract]
        List<Station> GetStationsByContract(string contract);

        [OperationContract]
        List<Station> GetAllStations();
        
    }
}
