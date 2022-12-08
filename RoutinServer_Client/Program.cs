using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutingServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            RoutingServiceClient client = new RoutingServiceClient();
            double value1 = 39.4608480566945D;
            double value2 = -0.3954071515343D;
            GeoCoordinates location = new GeoCoordinates(value1, value2);
            client.GetContractNameCompleted += new EventHandler<GetContractNameCompletedEventArg>(GetContractNameCompletedCallBack);
            client.GetContractNameAsync(value1, value2);

            client.Close();
        }

        static void GetContractNameCompletedCallBack(object sender, GetContractNameCompletedEventArg e)
        {
            Console.WriteLine("Contract name: {0}", e.Result);
        }

    }
}
