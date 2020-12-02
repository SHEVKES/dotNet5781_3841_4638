using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_002_3841_4638
{
   public class BusStation
    {
        static Random rand = new Random();
        private static int code = 0;
        protected int busCode;
        protected double latitude;
        protected double longitude;
        protected string busAdress;
        public int BusCode
        {
            get { return busCode; }
            set 
            {
                if (busCode <= code)
                    busCode = value;
                else
                    throw new BusStationException("ERROR,code incorrect");
            }
        }
        public double Latitude
        {
            get { return latitude; }
            set
            { 
                if (value >= 31 && value <= 33.3)
                    latitude = value;
                else
                    throw new BusStationException("ERROR, Latitude incorrect");
            }
        }
        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value >= 34.3 && value <= 35.5)
                    longitude = value;
                else
                    throw new BusStationException("ERROR, Longitude incorrect");
            }
        }
        public string BusAdress
        {
            get { return busAdress; }
            set { busAdress = value; }
        }
        public BusStation(string busAdress=" ")//ctor
        {
            busCode = code++;
            latitude = rand.NextDouble() * (33.3 - 31) + 31;
            longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3;
            this.busAdress = busAdress;
        }
        public BusStation(BusStation bus)//copy cotr
        {
            this.busCode = bus.BusCode;
            this.latitude = bus.Latitude;
            this.longitude = bus.longitude;
            this.busAdress = bus.BusAdress;
        }
        public override string ToString()
        {
            return "Bus Station Code: "+busCode+", "+latitude+"°N "+longitude+"°E";
        }
    }
   
}
