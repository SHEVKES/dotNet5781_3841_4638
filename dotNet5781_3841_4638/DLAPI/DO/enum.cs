using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum BusStatus //enum for the bus status
    {
        AVAILABLE, InTRAVEL, REFUELING, TREATMENT
    }
    public enum Area //enum for the lines areas
    {
        General = 1, North, South, Center, Jerusalem
    }
}
