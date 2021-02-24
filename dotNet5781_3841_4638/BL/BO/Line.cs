using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Line
    {
        public int LineId { get; set; } //id of the line
        public int LineNum { get; set; } //number of the line
        public Area Area { get; set; } //area of the line
        public List<TimeSpan> DepTimes { get; set; }//departure time
        public List <StationInLine> stations { get; set; }
        public override string ToString()
        {
            //return ":מזהה קו" + LineId + ":מספר קו" + LineNum + ":אזור" + Area;                    
            
            return string.Format("{0} {1} {2}",
                      Area, LineNum, LineId);
        }
    }
}
