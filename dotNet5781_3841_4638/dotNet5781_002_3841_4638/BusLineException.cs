using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace dotNet5781_002_3841_4638
{
    class BusLineException:Exception
    {
        public BusLineException() : base() { }
        public BusLineException(string message) : base(message) { }
        public BusLineException(string massage, Exception inner) : base(massage, inner) { }
        protected BusLineException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
