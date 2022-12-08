using System;
using System.Collections;
using System.Diagnostics;
using System.Device.Location;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Collections.Generic;

namespace RoutingServer.RestClients.OpenStreetMap.Map
{
    public class OpenStreetMapDirectionsClient : RestClient
    {
        private static OpenStreetMapDirectionsClient instance;

        public OpenStreetMapDirectionsClient(String APIKey, Uri baseAddress) : base(APIKey, baseAddress) { }

        public static OpenStreetMapDirectionsClient GetClient(String APIKey, Uri baseAddress)
        {
            if (instance == null)
            {
                instance = new OpenStreetMapDirectionsClient(APIKey, baseAddress);
            }
            return instance;
        }
        
        public async Task<Itinerary> GetItineraryByFoot(GeoCoordinate origin, GeoCoordinate destination)
        {
            try
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = new Data()
                {
                    coordinates = new double[][] { 
                        new double[] { origin.Longitude, origin.Latitude }, 
                        new double[] { destination.Longitude, destination.Latitude } 
                    },
                    maneuvers = true
                };
                var jsonData = JsonConvert.SerializeObject(data);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(new Uri(client.BaseAddress + "foot-walking"), contentData);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Itinerary>(responseBody);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
            
        }

        public async Task<Itinerary> GetItineraryByBicycle(GeoCoordinate origin, GeoCoordinate destination)
        {
            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var data = new Data()
                {
                    coordinates = new double[][] { 
                        new double[] { origin.Longitude, origin.Latitude },
                        new double[] { destination.Longitude, destination.Latitude } 
                    },
                    maneuvers = true
                };
                
                var jsonData = JsonConvert.SerializeObject(data);
                var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(new Uri(client.BaseAddress + "cycling-regular"), contentData);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Itinerary>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return null;
            }
        }

        class Data
        {
            public double[][] coordinates { get; set; }
            public bool maneuvers { get; set; }
        }
    }
}
