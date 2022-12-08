using System;
using System.Web;
using System.Net.Http;
using ModernHttpClient;
using System.Collections;
using System.Threading.Tasks;

namespace ProxyServer.JCDECAUXRestClient
{
    public class RestClient
    {
        private static RestClient instance;
        private HttpClient client;
        private readonly string apiKey;

        public static RestClient GetClient(String APIKey)
        {
            if(instance == null)
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
            client.BaseAddress = new Uri("https://api.jcdecaux.com/vls/v1/stations");
            client.MaxResponseContentBufferSize = 2147483647;
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

        public async Task<string> GetAllContracts()
        {
            string responseBody = "";
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "apiKey", apiKey.ToString() }
                };

                var url = buildQuery(parameters, client.BaseAddress);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }

        public async Task<string> GetStationsByContract(string contract)
        {
            string responseBody = "";
            HttpResponseMessage response;
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "apiKey", apiKey.ToString() },
                    { "contract", contract }
                };

                var url = buildQuery(parameters, client.BaseAddress);
                client.DefaultRequestHeaders.Add("User-Agent", "WHATEVER VALUE");
                response = await client.GetAsync(url);
                Console.WriteLine("Reponse : " + response.Content + response.Headers + response.ToString());
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                
            }
            return responseBody;
        }

        public async Task<string> GetStationInformation(string contract, int station)
        {
            string responseBody = "";
            try
            {
                Hashtable parameters = new Hashtable
                {
                    { "apiKey", apiKey.ToString() },
                    { "contract", contract }
                };
                
                Uri apiUri = new Uri(client.BaseAddress.ToString() + "/" + station.ToString());
                var url = buildQuery(parameters, apiUri);
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }
    }
}
