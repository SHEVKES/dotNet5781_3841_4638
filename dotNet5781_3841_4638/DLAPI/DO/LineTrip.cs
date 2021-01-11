using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        public int LineTripId { get; set; } //id of the 
        public int LineId { get; set; } //id of the line
        public TimeSpan StartAt { get; set; } //time of the start
        public TimeSpan Frequency { get; set; } //frequency
        public TimeSpan FinishAt { get; set; } //time of the end
        public bool IsDeleted { get; set; } //check if the line trip has deleted
    }
}
