using System;
using System.ServiceModel;


namespace ProxyServer_Host
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ProxyServer.ProxyService));
            host.Open();
            Console.WriteLine("Service Hosted Sucessfully");
            Console.Read();
        }
    }
}
