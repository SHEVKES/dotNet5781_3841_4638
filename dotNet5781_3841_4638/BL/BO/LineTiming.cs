using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    // The class represents a bus (of certain line) arriving soon to the bus station
    public class LineTiming
    {
       //private static int counter = 0;
       //public int ID;
       //public LineTiming() => ID = ++counter; //unique
       //public TimeSpan TripStart { get; set; } //time of Line start the trip, taken from StartAt of LineTrip
        public int LineId { get; set; } //Line ID from Line
        public int LineNum { get; set; } //Line Number as understood by the people
        public string DestinationStation { get; set; }// Last station name - so the passengers will know better which direction it is
        //public TimeSpan ExpectedTimeTillArrive { get; set; }//Expected time of arrival
        public string Stringtimes { get; set; }//string of the times of the line in the closer hour
        public override string ToString()
        {
            return LineId + "   " + LineNum + "   " + DestinationStation + "   " + Stringtimes;
        }
    }
}
