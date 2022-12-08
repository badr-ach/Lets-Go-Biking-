using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.ServiceReference1;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoutingServiceClient client = new RoutingServiceClient();
            GeoCoordinates geolocation = new GeoCoordinates();
            geolocation.Latitude = new decimal(39.460848056694);
            geolocation.Longitude = new decimal(-0.3954071515343);
            client.GetContractNameCompleted += new EventHandler<GetContractNameCompletedEventArgs>(GetCallBack);
            client.GetContractNameAsync(geolocation);
            
            void GetCallBack(object sender, GetContractNameCompletedEventArgs e)
            {
                try
                {
                    Console.WriteLine("Res : {0}", e.Result);
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            Console.ReadLine();
        }

    }
}
