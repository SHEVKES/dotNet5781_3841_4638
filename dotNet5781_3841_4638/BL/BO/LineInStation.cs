using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInStation
    {
        public int LineId { get; set; } //id of the line
        public int LineNum { get; set; } //number of the line
        public int LineStationIndex { get; set; } //the index of the station in the line
    }
}
