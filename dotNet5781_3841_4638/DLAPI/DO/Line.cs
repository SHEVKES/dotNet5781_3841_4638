using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        public int LineId { get; set; } //id of the line
        public int LineNum { get; set; } //number of the line
        public Area Area { get; set; } //area of the line
        public int FirstStation { get; set; } // number of the first station
        public int LastStation { get; set; } // number of the last station
        public bool IsDeleted { get; set; } //check if the line has deleted
    }
}
