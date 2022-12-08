using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.RestClients.OpenStreetMap.Map
{
    [DataContract]
    public class GeoCode
    {
        [DataMember]
        public Feature[] features { get; set; }

        public GeoCoordinate GetGeoCoordinate()
        {
            return new GeoCoordinate()
            {
                Latitude = features[0].geometry.coordinates[1],
                Longitude = features[0].geometry.coordinates[0]
            };
        }

        public string GetLocality()
        {
            return features[0].properties.locality;
        }

        public string GetMacroCounty()
        {
            return features[0].properties.macrocounty;
        }
    }

    [DataContract]
    public class Feature
    {
        [DataMember]
        public Properties properties { get; set; }
        [DataMember]
        public Geometry geometry { get; set; }
    }

    [DataContract]
    public class Properties
    {
        [DataMember]
        public string macrocounty { get; set; }
        [DataMember]
        public string locality { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember]
        public double[] coordinates { get; set; }
    }


}
