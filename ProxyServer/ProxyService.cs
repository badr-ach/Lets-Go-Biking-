using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.Caching;
using System;
using System.Linq;
using ProxyServer.JCDECAUXRestClient;

namespace ProxyServer
{
    public class ProxyService : IProxyService
    {
        GenericProxyCache<Contract> GenericProxycache = new GenericProxyCache<Contract>(12,0,0);

        public List<Station> GetStationsByContract(string contract)
        {
            return GenericProxycache.Get(contract).Item;
        }
    }
}
