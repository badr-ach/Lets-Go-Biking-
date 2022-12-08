using System;
using System.ServiceModel;

namespace RoutingServer_Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(RoutingServer.RoutingService));
            host.Open();
            Console.WriteLine("Service Hosted Sucessfully");
            Console.Read();

        }
    }
}
