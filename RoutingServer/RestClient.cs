using System;
using System.Web;
using System.Net.Http;
using ModernHttpClient;
using System.Collections;
using System.Threading.Tasks;
using System.Text.Json;

namespace RoutingServer
{
    internal class RestClient
    {
        private static RestClient instance;
        private HttpClient client;
        private readonly string apiKey;

        public static RestClient getClient(String APIKey)
        {
            if (instance == null)
            {
                instance = new RestClient(APIKey);
            }
            return instance;
        }

        private RestClient(String APIKey)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("https://api.openrouteservice.org/v2/directions/");
            apiKey = APIKey;
        }

        private string buildQuery(Hashtable parameters, Uri baseAddress)
        {
            var builder = new UriBuilder(baseAddress);
            var query = HttpUtility.ParseQueryString(builder.Query);
            foreach (DictionaryEntry pair in parameters)
            {
                query[pair.Key.ToString()] = pair.Value.ToString();
            }
            builder.Query = query.ToString();
            string url = builder.ToString();
            return url;
        }

        public async Task<Itinerary> GetItineraryByFoot(GeoCoordinates origin, GeoCoordinates destination)
        {
            string responseBody = "";
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "apiKey", apiKey.ToString() },
                    { "start", origin.ToString() },
                    { "end", origin.ToString() }
                };

                var url = buildQuery(parameters, new Uri(client.BaseAddress + "foot-walking?"));

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return JsonSerializer.Deserialize<Itinerary>(responseBody);
        }

        public async Task<Itinerary> GetItineraryByBicycle(GeoCoordinates origin, GeoCoordinates destination)
        {
            string responseBody = "";
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "apiKey", apiKey.ToString() },
                    { "start", origin.ToString() },
                    { "end", origin.ToString() }
                };

                var url = buildQuery(parameters, new Uri(client.BaseAddress + "cycling-road?"));

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return JsonSerializer.Deserialize<Itinerary>(responseBody);
        }
    }
}
