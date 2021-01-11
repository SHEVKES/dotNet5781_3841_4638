using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class LineStation
    {
        public int LineId { get; set; } //id of the line
        public int StationCode { get; set; } //code of the station
        public int LineStationIndex { get; set; } //the index of the station in the line
        public int PrevStationCode { get; set; } //code of the previos station
        public int NextStationCode { get; set; } //code of the next station
        public bool IsDeleted { get; set; } //check if the line station has deleted
    }
}
