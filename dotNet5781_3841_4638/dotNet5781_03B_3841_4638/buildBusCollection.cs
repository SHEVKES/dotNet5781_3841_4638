using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using dotNet5781_01_3841_4638;
/// <summary>
/// In this department we initialize 10 different bus lines.
/// </summary>
namespace dotNet5781_03B_3841_4638
{
    public static class buildBusCollection
    {
        public static void RestartBuses(ObservableCollection<Bus>buses)
        {
            buses.Add(new Bus("24687531", new DateTime(2018, 11, 24), 15000, 1200, new DateTime(2020, 10, 3), 2000));
            buses.Add(new Bus("12345678", new DateTime(2019, 1, 13), 10000, 1000, new DateTime(2020, 11, 4), 3000));
            buses.Add(new Bus("7654321", new DateTime(2015, 12, 3), 10000, 800, new DateTime(2020, 7, 15), 4520));
            buses.Add(new Bus("7766552", new DateTime(2017, 10, 10), 15000, 950, new DateTime(2020, 8, 22), 1530));
            buses.Add(new Bus("86457311", new DateTime(2018, 9, 27), 10000, 1200, new DateTime(2020, 1, 4), 3000));
            buses.Add(new Bus("5674321", new DateTime(2016, 11, 13), 13000, 1100, new DateTime(2020, 10, 16), 500));
            buses.Add(new Bus("67011347", new DateTime(2019, 10, 12), 5000, 1200, new DateTime(2020, 12, 5), 300));
            buses.Add(new Bus("54689763", new DateTime(2019, 12, 1), 6000, 1, new DateTime(2020, 9, 1), 1200)); //need refuel
            buses.Add(new Bus("8967422", new DateTime(2016, 4, 24), 10000, 1000, new DateTime(2019, 11, 21), 8700));//need treatment-date
            buses.Add(new Bus("1000247", new DateTime(2017, 8, 2), 30000, 800, new DateTime(2020, 5, 15), 10001));//need treatment-20000
        }
    }
}
