using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace RoutingServer
{
    [DataContract]
    public class Itinerary
    {

        [DataMember]
        public Route[] routes { get; set; }

        [DataMember]
        public double[] bbox { get; set; }


        [DataMember]
        public bool found { get; set; }

        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string mode { get; set; }

        public double GetDuration()
        {
            return routes[0].summary.duration;
        }

        public double GetDistance()
        {
            return routes[0].summary.distance;
        }

        public string GetGeometry()
        {
            return routes[0].geometry;
        }

        public Step[] GetSteps()
        {
            Step[] steps = routes[0].segments.SelectMany(s => s.steps).ToArray(); 
            return steps;
            
        }
    }

    [DataContract]
    public class Route
    {
        [DataMember]
        public string geometry { get; set; }
        [DataMember]
        public Summary summary { get; set; }
        [DataMember]
        public Segment[] segments { get; set; }
    }


    [DataContract]
    public class Segment
    {
        [DataMember]
        public double distance { get; set; }
        [DataMember]
        public double duration { get; set; }
        [DataMember]
        public Step[] steps { get; set; }
    }

    [DataContract]
    public class Step
    {
        [DataMember]
        public double distance { get; set; }
        [DataMember]
        public double duration { get; set; }
        [DataMember]
        public int type { get; set; }

        [DataMember]
        public string instruction { get; set; }
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public Maneuver maneuver { get; set; }

        [DataMember]
        public int[] way_points { get; set; }
    }

    [DataContract]
    public class Summary
    {
        [DataMember]
        public double distance { get; set; }
        [DataMember]
        public double duration { get; set; }
    }

    [DataContract]
    public class Maneuver
    {
        [DataMember]
        public double[] location { get; set; }
    }


    [DataContract]
    public class Coordinates
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

}
