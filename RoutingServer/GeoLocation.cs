using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    [DataContract]
    public class GeoCoordinates
    {
        [DataMember]
        public double Latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }


        public override string ToString()
        {
            return Latitude.ToString() + "," + Longitude.ToString();
        }
    }
}
