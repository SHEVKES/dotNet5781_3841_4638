using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_3841_4638
{
    public class Bus
    {
        public string licenseNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime lastTreatment { get; set; } //The last date the bus received treatment
        public double TotalKm { get; set; }
        public double lastKmTreat { get; set; } //The amount of miles traveled since the last treatment
        public double fuel { get; set; }
        public static bool check(string c) //Check if the license number is correct
        {
            int check;
            if (Int32.TryParse(c, out check))
            {
                return true;
            }
            return false;
        }
        public bool CanTravel(double km)
        {
            if (((TotalKm - lastKmTreat) + km >= 20000) || ((DateTime.Now - lastTreatment).TotalDays >= 365))
            {
                Console.WriteLine(" The bus needs treatment");
                return false;
            }
            if (fuel - km < 0)
            {
                Console.WriteLine(" The bus is missing fuel");
                return false;
            }
            return true;
        }
        public void Refuel()
        {
            fuel = 1200;
        }
        public void Treatment()
        {
            lastTreatment = DateTime.Now;
            lastKmTreat = TotalKm;
        }
        public override string ToString() //Displays the mileage and license number of the bus.
        {
            string prefix, middle, suffix;
            if (StartDate.Year < 2018)
            {
                prefix = licenseNumber.Substring(0, 2);
                middle = licenseNumber.Substring(2, 3);
                suffix = licenseNumber.Substring(5, 2);
            }
            else
            {
                prefix = licenseNumber.Substring(0, 3);
                middle = licenseNumber.Substring(3, 2);
                suffix = licenseNumber.Substring(5, 3);
            }
            string finalLicNum = String.Format("{0}-{1}-{2}", prefix, middle, suffix);
            return "License Number: " + finalLicNum + " Total KM: " + TotalKm;

        }
    }
}
