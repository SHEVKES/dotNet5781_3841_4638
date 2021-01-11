using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; } //license number
        public DateTime FromDate { get; set; } //date of start
        public double TotalTrip { get; set; } //total km
        public double FuelRemain { get; set; } //fuel
        public BusStatus Status { get; set; } //status of the bus
        public DateTime DateLastTreat { get; set; } //date of the last treatment
        public double KmLastTreat { get; set; } // total km from the last treatment
        public override string ToString()
        {
            return LicenseNum.ToString();
        }
    }
}
