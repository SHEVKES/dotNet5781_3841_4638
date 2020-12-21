using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Permissions;
using System.Windows;

namespace dotNet5781_03B_3841_4638
{
   public class Bus: DependencyObject
    {
        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (value <= DateTime.Now)
                    startDate = value;
                else
                    throw new BusException("ERROR,your date does not exist");
            }
        }
        private string licenseNumber; 

        public string LicenseNumber
        {
            get { return this.OrderLicenseNumber(); }
            set
            {
                int x;
                bool flag = int.TryParse(value, out x);
                if (flag)
                {
                    if ((value.Length == 7 && startDate.Year < 2018) || (value.Length == 8 && startDate.Year >= 2018))
                        licenseNumber = value;
                    else
                        throw new BusException("The year or the license number is incorrect");
                }
                else
                    throw new BusException("The license number is incorrect");
            }
        }
        private DateTime lastTreatment; //The last date the bus received treatment

        public DateTime LastTreatment
        {
            get { return lastTreatment; }
            set
            {
                if (value <= DateTime.Now && value >= startDate)
                    lastTreatment = value;
                else
                    throw new BusException("ERROR,your date does not exist");
            }
        }
        private double totalKm;  

        public double TotalKm
        {
            get { return totalKm; }
            set
            {
                if (totalKm <= value)
                    totalKm = value;
                else
                    throw new BusException("ERROR, your total Kilometrage is incorrect");

            }
        }
        private double lastKmTreat; //The amount of miles traveled since the last treatment

        public double LastKmTreat
        {
            get { return lastKmTreat; }
            set
            {
                if (value >= 0 && value <= totalKm)
                    lastKmTreat = value;
                else
                    throw new BusException("ERROR, your kilometerage at the last treatment is incorrect");
            }
        }
        private double fuel;

        public double Fuel
        {
            get { return fuel; }
            set
            {
                if (value >= 0 && value <= 1200)
                    fuel = value;
                else
                    throw new BusException("ERROR,your fuel is incorrect");
            }
        }
        public Status BusStatus { get; set; }
        public Bus(string licenseNumber, DateTime startDate, double totalKm, double fuel, DateTime lastTreatment, double lastKmTreat)
        {
            this.startDate = startDate;
            this.licenseNumber = licenseNumber;
            this.lastTreatment = lastTreatment;
            if (totalKm < 0)
                throw new BusException("ERROR, total kilometrage is incorrect");
            this.totalKm = totalKm;
            this.Fuel = fuel;
            this.lastKmTreat = lastKmTreat;
            this.BusStatus = Status.Available;
        } //const
        public string OrderLicenseNumber()
        {
            string prefix, middle, suffix;
            if (startDate.Year < 2018)
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
            return finalLicNum;
        }
        public override string ToString()
        {
            return "License Number: " + licenseNumber + "\nTotal Kilometrage: " + totalKm + "\nDate Start: " + startDate + "\nFuel: " + fuel + "\nDate of last Treatment: " + lastTreatment + "\nKilometrage from last treatment: " + lastKmTreat + "\nStatus: " + BusStatus;
        }
        public Boolean NeedTreatment(double km = 0)
        {
            if (((totalKm - lastKmTreat) + km >= 20000) || ((DateTime.Now - lastTreatment).TotalDays >= 365))
            {
                return true;
            }
            return false;
        }
        public Boolean NeedFuel(double km = 0)
        {
            if (fuel - km < 0)
            {
                return true;
            }
            return false;
        }
        public bool CanTravel(double km)
        {
            if (NeedTreatment(km))
            {
                throw new BusException("The bus " + licenseNumber + " needs tratment");
            }
            if (NeedFuel(km))
            {
                throw new BusException("The bus " + licenseNumber + " is missing fuel");
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
            lastKmTreat = totalKm;
        }
        public void DoRide(double km)
        {
            if (CanTravel(km))
            {
                totalKm += km;
                fuel -= km;
            }
        }
        public Boolean IsBusy()
        {
            return (BusStatus != Status.Available);
        }
    }
}
