using RoutingServer.Contracts;
using RoutingServer.RestClients.OpenStreetMap.Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer.RestClients.OpenStreetMap.Nominatim
{
    public class OpenStreetMapNomatimClient : RestClient
    {
        private static OpenStreetMapNomatimClient instance;

        public OpenStreetMapNomatimClient(String APIKey, Uri baseAddress) : base(APIKey, baseAddress) { }

        public static OpenStreetMapNomatimClient GetClient(String APIKey, Uri baseAddress)
        {
            if (instance == null)
            {
                instance = new OpenStreetMapNomatimClient(APIKey, baseAddress);
            }
            return instance;
        }

        public async Task<Place> GetGeoCode(string address)
        {
            try
            {
                var url = new Uri(client.BaseAddress + "search?format=json&addressdetails=1&q=\"" + address + "\"");
                client.DefaultRequestHeaders.Add("User-Agent", "HERE");
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Place[]>(responseBody)[0];

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }

        }
    }
}
