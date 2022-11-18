using ProxyServer.JCDECAUXRestClient;
using System.Collections.Generic;
using System.ServiceModel;

namespace ProxyServer
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IProxyService
    {

        [OperationContract]
        List<Station> GetStationsByContract(string contract);

    }
}
