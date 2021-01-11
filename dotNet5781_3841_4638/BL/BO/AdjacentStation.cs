using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class AdjacentStation
    {
        public int StationCode1 { get; set; } //code of first station 
        public int StationCode2 { get; set; } //code of second station 
        public double Distance { get; set; } //distance between two adjacent stations
        public TimeSpan Time { get; set; } //average travel time

    }
}
