using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
     public class AdjacentStations
    {
        public int StationCode1 { get; set; } //code of first station 
        public int StationCode2 { get; set; } //code of second station 
        public double Distance { get; set; } //distance between two adjacent stations
        public TimeSpan Time { get; set; } //average travel time
    }
}
