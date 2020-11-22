using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_002_3841_4638
{
    class BusLineStation : BusStation, IComparable<BusLineStation>
    {
        static Random rand = new Random();
        private double distance;
        private TimeSpan travelTime;
        public double Distance
        {
            get { return distance; }
            set
            {
                if (value > 0)
                    distance = value;
                else
                    throw new BusStationException("ERROR, Distance incorrect");
            }
        }
        public TimeSpan TravelTime
        {
            get { return travelTime; }
            set { travelTime = value; }
        }
        public BusLineStation(TimeSpan travelTime, string busAdress = ""):base(busAdress)//ctor
        {
            this.distance = rand.NextDouble() * (500);
            this.travelTime = travelTime;
        }
        public BusLineStation(BusStation bus, TimeSpan time) : base(bus)//copy cotr
        {
            this.distance = rand.NextDouble() * (500);
            this.travelTime = time;
        }
        public int CompareTo(BusLineStation obj)
        {
            return this.BusCode.CompareTo(obj.BusCode);
        }
    }
}
