using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProxyServer.JCDECAUXRestClient
{
    [DataContract]
    public class Station
    {
        [DataMember]
        public int number { get; set; }
        [DataMember]
        public string contract_name { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public Position position { get; set; }
        [DataMember]
        public bool banking { get; set; }
        [DataMember]
        public bool bonus { get; set; }
        [DataMember]
        public int bike_stands { get; set; }
        [DataMember]
        public int available_bike_stands { get; set; }
        [DataMember]
        public int available_bikes { get; set; }
        [DataMember]
        public string status { get; set; }
/*        [DataMember]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime last_update { get; set; }*/

    }

    [DataContract]
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
