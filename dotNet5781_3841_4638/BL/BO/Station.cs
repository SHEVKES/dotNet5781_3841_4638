using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public int Code { get; set; } //code of the station
        public string Name { get; set; } // name of the station
        public string Address { get; set; } //address of station
        public bool DisabledAccess { get; set; } //access to disabled
        public List<LineInStation> Lines { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
