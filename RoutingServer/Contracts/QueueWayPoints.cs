using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoutingServer.Contracts
{
    [DataContract]
    public class QueueWayPoints
    {
        [DataMember]
        public string queuename { get; set; }

        [DataMember]
        public string currentMode { get; set; }

        [DataMember]
        public string geometry { get; set; }

        [DataMember]
        public bool found { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}
