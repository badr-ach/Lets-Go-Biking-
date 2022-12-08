using System;
using System.Web;
using System.Net.Http;
using ModernHttpClient;
using System.Collections;
using System.Threading.Tasks;
using System.Text.Json;
using System.Web.UI.WebControls;

namespace RoutingServer
{
    public abstract class RestClient
    {
        protected readonly string apiKey;
        protected HttpClient client;

        public RestClient(String APIKey, Uri baseAddress)
        {   
            apiKey = APIKey;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = baseAddress;
            client.MaxResponseContentBufferSize = 2147483647;
        }

        public string buildQuery(Hashtable parameters, Uri baseAddress)
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
        
    }
}
