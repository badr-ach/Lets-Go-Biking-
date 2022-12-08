using System;
using System.Collections;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoutingServer.RestClients.OpenStreetMap.Map
{
    public class OpenStreetMapGeoCodeClient : RestClient
    {
        private static OpenStreetMapGeoCodeClient instance;

        public OpenStreetMapGeoCodeClient(String APIKey, Uri baseAddress) : base(APIKey, baseAddress) { }

        public static OpenStreetMapGeoCodeClient GetClient(String APIKey, Uri baseAddress)
        {
            if (instance == null)
            {
                instance = new OpenStreetMapGeoCodeClient(APIKey, baseAddress);
            }
            return instance;
        }

        public async Task<GeoCode> GetGeoCode(string lon, string lat )
        {
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "api_key",apiKey },
                    { "size", 1 },
                    { "point.lon",lon },
                    { "point.lat",lat }
                };

                var url = buildQuery(parameters, new Uri(client.BaseAddress + "reverse?"));

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GeoCode>(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }

        public async Task<GeoCode> GetAddress(String address)
        {
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "api_key",apiKey},
                    { "text", address }
                };

                var url = buildQuery(parameters, new Uri(client.BaseAddress + "autocomplete?"));

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<GeoCode>(responseBody);

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
