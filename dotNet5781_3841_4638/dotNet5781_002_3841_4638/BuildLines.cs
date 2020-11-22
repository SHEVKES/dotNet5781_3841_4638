using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_002_3841_4638
{
    class BuildLines
    {
        static public void createLists(ref List<BusStation> stations, BusLineCollection lines)
        {
            stations = new List<BusStation>();
            for (int i = 0; i < 40; i++)
            {
                stations.Add(new BusStation());
            }
        }
    }
}
