using System.ComponentModel;
using System.Device.Location;
using System.Runtime.Serialization;
using System.ServiceModel;

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
        
        [DataContract]
        public class Position
        {
            [DataMember]
            public double lat { get; set; }
            [DataMember]
            public double lng { get; set; }
        }
    }

    
}
