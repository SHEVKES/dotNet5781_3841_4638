using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet5781_002_3841_4638
{
    class BusStationException: Exception
    {
        public BusStationException(): base() { }
        public BusStationException(string message) : base(message) { }
        public BusStationException(string massage, Exception inner) : base(massage, inner) { }
        protected BusStationException( SerializationInfo info,StreamingContext context): base(info, context) { }
    }
}
