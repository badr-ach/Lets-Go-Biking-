using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyServer.JCDECAUXRestClient
{
    public class Contract
    {
        public List<Station> Item { get; set; }
        
        readonly RestClient restfulClient = RestClient.getClient("4bcb8209b5c28582e240139eddf4642cd0c7e690");
        
    
        public Contract(string key)
        {
            
            // blocks here
            Task<string> response = restfulClient.GetStationsByContract(key);
            
            response.Wait();
            
            Item = JsonSerializer.Deserialize<List<Station>>(response.Result);
        }         
          
        
        
    }
}
