using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProxyServer.JCDECAUXRestClient
{
    public class Contract
    {
        public List<Station> Item { get; set; }
        
        readonly RestClient restfulClient = RestClient.GetClient("4bcb8209b5c28582e240139eddf4642cd0c7e690");
        
    
        public Contract(string key)
        {
            String response = "";

            if (key.Equals("ALL"))
            {
                response = restfulClient.GetAllContracts().Result;
            }
            else
            {
                response = restfulClient.GetStationsByContract(key).Result;
                Console.WriteLine(response);
                Debug.WriteLine(response);
            }

            try
            {
                Item = JsonSerializer.Deserialize<List<Station>>(response);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                
                Item = new List<Station> { };
            }
        }         
          
        
        
    }
}
