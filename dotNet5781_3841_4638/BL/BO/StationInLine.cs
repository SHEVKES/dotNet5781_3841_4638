using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInLine
    {
        public int StationCode { get; set; } //code of the station
        public string Name { get; set; } // name of the station
        public bool DisabledAccess { get; set; } //access to disabled
        public int LineStationIndex { get; set; } //the index of the station in the line
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public override string ToString()
        {
            //return "Station code: " + StationCode + "  Line station index: " + LineStationIndex + "  Name: " + Name;
            return string.Format("{0} {1})",
                      StationCode, Name);
            
        }

    }


}
