
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Trip
    {
        public int TripId { get; set; } //id of the trip
        public string UserName { get; set; } //user name
        public int LineId { get; set; } //id of the line
        public int GetOnStation { get; set; } //id of the 
        public TimeSpan GetOnTime { get; set; }
        public int GetOffStation { get; set; } //id of the 
        public TimeSpan GetOffTime { get; set; }
    }
}
