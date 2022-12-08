using Amqp;
using RoutingServer.RestClients.OpenStreetMap.Map;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Contracts
{
    [DataContract]
    public class Place
    {
		public string lat { get; set; }
		public string lon { get; set; }
        public string display_name { get; set; }

        public Address address { get; set; }
        
        public Geometry GetGeometry()
        {
            return new Geometry()
            {
                coordinates = new double[] { Double.Parse(lon.Replace('.',',')), Double.Parse(lat.Replace('.', ',')) }
            };
        }

        public string GetLocality()
        {
            return address.municipality;
        }

        public string GetMacroCounty()
        {
            return address.county;
        }
    }

    public class Address
    {
        public string town { get; set; }
        public string municipality { get; set; }
        public string county { get; set; }
    }
}

