using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_002_3841_4638
{
     public class BuildLines
    {
        static public void createLists(ref List<BusStation> stations, BusLineCollection lines)
        {
            stations = new List<BusStation>();

            for (int i = 0; i < 40; i++)
            {
                stations.Add(new BusStation());
            }
            lines.AddLineToCollection(new BusLine(280, new List<BusLineStation>() { new BusLineStation(stations[0], TimeSpan.Zero), new BusLineStation(stations[1], new TimeSpan(00, 01, 20)), new BusLineStation(stations[2], new TimeSpan(00, 05, 20)), new BusLineStation(stations[3], new TimeSpan(00, 04, 10)), new BusLineStation(stations[39], new TimeSpan(00, 04, 20)) }, Areas.General));
            lines.AddLineToCollection(new BusLine(400, new List<BusLineStation>() { new BusLineStation(stations[4], TimeSpan.Zero), new BusLineStation(stations[5], new TimeSpan(00, 03, 20)), new BusLineStation(stations[6], new TimeSpan(00, 05, 12)), new BusLineStation(stations[7], new TimeSpan(00, 01, 10)), new BusLineStation(stations[0], new TimeSpan(00, 07, 00)) }, Areas.Jerusalem));
            lines.AddLineToCollection(new BusLine(11, new List<BusLineStation>() { new BusLineStation(stations[8], TimeSpan.Zero), new BusLineStation(stations[9], new TimeSpan(00, 05, 00)), new BusLineStation(stations[10], new TimeSpan(00, 04, 20)), new BusLineStation(stations[11], new TimeSpan(00, 04, 00)), new BusLineStation(stations[14], new TimeSpan(00, 03, 20)) }, Areas.General));
            lines.AddLineToCollection(new BusLine(80, new List<BusLineStation>() { new BusLineStation(stations[12], TimeSpan.Zero), new BusLineStation(stations[13], new TimeSpan(00, 01, 00)), new BusLineStation(stations[14], new TimeSpan(00, 03, 02)), new BusLineStation(stations[15], new TimeSpan(00, 02, 10)), new BusLineStation(stations[7], new TimeSpan(00, 01, 20)) }, Areas.South));
            lines.AddLineToCollection(new BusLine(51, new List<BusLineStation>() { new BusLineStation(stations[16], TimeSpan.Zero), new BusLineStation(stations[17], new TimeSpan(00, 04, 03)), new BusLineStation(stations[18], new TimeSpan(00, 01, 20)), new BusLineStation(stations[19], new TimeSpan(00, 01, 10)), new BusLineStation(stations[13], new TimeSpan(00, 04, 06)) }, Areas.General));
            lines.AddLineToCollection(new BusLine(89, new List<BusLineStation>() { new BusLineStation(stations[20], TimeSpan.Zero), new BusLineStation(stations[21], new TimeSpan(00, 01, 23)), new BusLineStation(stations[22], new TimeSpan(00, 07, 20)), new BusLineStation(stations[23], new TimeSpan(00, 04, 05)), new BusLineStation(stations[6], new TimeSpan(00, 03, 01)) }, Areas.North));
            lines.AddLineToCollection(new BusLine(70, new List<BusLineStation>() { new BusLineStation(stations[24], TimeSpan.Zero), new BusLineStation(stations[25], new TimeSpan(00, 08, 20)), new BusLineStation(stations[26], new TimeSpan(00, 04, 03)), new BusLineStation(stations[27], new TimeSpan(00, 04, 10)), new BusLineStation(stations[1], new TimeSpan(00, 04, 20)) }, Areas.General));
            lines.AddLineToCollection(new BusLine(239, new List<BusLineStation>() { new BusLineStation(stations[28], TimeSpan.Zero), new BusLineStation(stations[29], new TimeSpan(00, 02, 20)), new BusLineStation(stations[30], new TimeSpan(00, 05, 05)), new BusLineStation(stations[31], new TimeSpan(00, 04, 18)), new BusLineStation(stations[2], new TimeSpan(00, 06, 29)) }, Areas.General));
            lines.AddLineToCollection(new BusLine(9, new List<BusLineStation>() { new BusLineStation(stations[32], TimeSpan.Zero), new BusLineStation(stations[33], new TimeSpan(00, 04, 59)), new BusLineStation(stations[34], new TimeSpan(00, 04, 54)), new BusLineStation(stations[35], new TimeSpan(00, 04, 45)), new BusLineStation(stations[11], new TimeSpan(00, 03, 20)) }, Areas.Jerusalem));
            lines.AddLineToCollection(new BusLine(13, new List<BusLineStation>() { new BusLineStation(stations[36], TimeSpan.Zero), new BusLineStation(stations[37], new TimeSpan(00, 06, 43)), new BusLineStation(stations[38], new TimeSpan(00, 05, 20)), new BusLineStation(stations[39], new TimeSpan(00, 04, 03)), new BusLineStation(stations[3], new TimeSpan(00, 02, 20)) }, Areas.Center));





        }
    }
}
