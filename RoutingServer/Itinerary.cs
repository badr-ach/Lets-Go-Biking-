using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer
{
    [DataContract]
    public class Itinerary
    {
        [DataMember]
        string type { get; set; }

        [DataMember]
        Feature[] features { get; set; }

        [DataMember]
        BBOX bbox { get; set; }

        [DataMember]
        Metadata metadata { get; set; }

    }

    public class Metadata
    {
        [DataMember]
        string attribution { get; set; }
        [DataMember]
        string service { get; set; }
        [DataMember]
        string timestamp { get; set; }
        [DataMember]
        string query { get; set; }
        [DataMember]
        Engine engine { get; set; }

        internal class Query
        {
            double[][] coordinates { get; set; }
        }
        internal class Engine
        {
            [DataMember]
            string version { get; set; }
            
            [DataMember]
            string build_date { get; set; }

            [DataMember]
            string graph_date { get; set; }
        }
    }
    

    [DataContract]
    public class Feature
    {
        [DataMember]
        BBOX bbox { get; set; }

        [DataMember]
        string type { get; set; }

        [DataMember]
        Properties properties { get; set; }

        [DataMember]
        Geometry geometry { get; set; }
    }

    [DataContract]
    public class BBOX
    {
        [DataMember]
        double startLongitude { get; set; }
        [DataMember]
        double startLatitude { get; set; }
        [DataMember]
        double endLongitude { get; set; }
        [DataMember]
        double endLatitude { get; set; }
    }

    [DataContract]
    public class Geometry
    {
        [DataMember]
        string type { get; set; }
        [DataMember]
        //Coordinates[] coordinates { get; set; }
        double[][] coordinates { get; set; }
    }

    [DataContract]
    public class Properties
    {
        [DataMember]
        Segment[] segments { get; set; }
        [DataMember]
        Summary summary { get; set; }
        [DataMember]
        int[] way_points { get; set; }
    }

    [DataContract]
    public class Segment
    {
        [DataMember]
        double distance { get; set; }
        [DataMember]
        double duration { get; set; }
        
    }

    [DataContract]
    public class Step
    {
        [DataMember]
        double distance { get; set; }
        [DataMember]
        double duration { get; set; }
        [DataMember]
        int type { get; set; }

        [DataMember]
        string instruction { get; set; }
        [DataMember]
        string name { get; set; }
        [DataMember]
        int[] way_points { get; set; }
    }

    [DataContract]
    public class Summary
    {
        [DataMember]
        double distance { get; set; }
        [DataMember]
        double duration { get; set; }
    }

    [DataContract]
    public class Coordinates
    {
        double longitude { get; set;  }
        double latitude { get; set; }
    }

}
