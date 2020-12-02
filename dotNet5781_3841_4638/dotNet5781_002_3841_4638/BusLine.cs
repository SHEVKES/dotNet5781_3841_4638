using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_002_3841_4638
{
    public class BusLine: IComparable<BusLine>
    { 
        public List<BusLineStation> busStations { get; set; }
        private int busNumber;
        private BusLineStation startingPoint;
        private BusLineStation endingPoint;
        private Areas area;
        public int BusNumber
        {
            get { return busNumber; }
            set
            {
                if (busNumber > 0)
                    busNumber = value;
                else
                    throw new BusLineException("The number of the bus line is invalid");
            }
        }
        public BusLineStation StartingPoint
        {
            get { return startingPoint; }
            set { startingPoint = value; }
        }
        public BusLineStation EndingPoint
        {
            get { return endingPoint; }
            set { endingPoint = value; }
        }
        public Areas Area
        {
            get { return area; }
            set { area = value; }
        }
        public BusLine(int busNumber, List<BusLineStation> busStations, Areas area)//ctor
        {
            if (busStations.Count < 2)
                throw new BusLineException("ERROR,there are not enough stations");
            if (busNumber < 0)
                throw new BusLineException("ERROR, the line does not exist");
            this.busNumber = busNumber;
            this.busStations = new List<BusLineStation>(busStations); ;
            this.area = area;
            this.startingPoint = this.busStations[0];
            this.endingPoint = this.busStations[busStations.Count - 1];
        }
        public override string ToString()
        {
            string line = "";
            foreach (BusLineStation item in busStations)
            {
                line += ("=>"+ item.BusCode);
            }
            return "Bus line:" + busNumber + "in area:" + area + "\nline:" + line;
        }
        public int Search(int code) //help function
        {
            int x = 0;
            foreach (BusLineStation item in busStations)
            {
                if(item.BusCode==code)
                {
                    return x;
                }
                x++;
            }
            return -1;
        }
        public void AddStation(BusLineStation other, Insert choice)
        {
            if (StationExist(other.BusCode))
                throw new BusLineException("the station is already exists");
            if (choice == Insert.Middle)
            {
                Console.WriteLine("Enter the number of the previous station");
                int prevStation;
                if (!int.TryParse(Console.ReadLine(), out prevStation))
                {
                    throw new BusLineException("ERROR");
                }
                int num = Search(prevStation);
                if (num == -1)
                {
                    throw new BusLineException("the previous station you entered doesn't exist");
                }
                Console.WriteLine("Enter the distance between the desired station and the station after it");
                double num2;
                while (!double.TryParse(Console.ReadLine(), out num2))
                {
                    Console.WriteLine("ERROR, please enter distance again");
                }
                Console.WriteLine("Enter the travel time between the desired station and the station after it");
                TimeSpan time;
                while (!TimeSpan.TryParse(Console.ReadLine(), out time))
                {
                    Console.WriteLine("ERROR,please enter time again");
                }
                busStations.Insert(++num, other);
                busStations[++num].Distance = num2;
                busStations[num].TravelTime = time;
                return;
            }
            if (choice == Insert.Last)
            {
                busStations.Add(other);
                endingPoint = other;
                return;
            }
            else
            {
                Console.WriteLine("Enter the distance between the desired station and the station after it");
                double num;
                while (!double.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("ERROR, please enter distance again");
                }
                Console.WriteLine("Enter the travel time between the desired station and the station after it");
                TimeSpan time;
                while (!TimeSpan.TryParse(Console.ReadLine(), out time))
                {
                    Console.WriteLine("ERROR, please enter time again");
                }
                busStations.Insert(0, other);
                startingPoint = other;
                busStations[0].Distance = 0;
                busStations[1].Distance = num;
                busStations[1].TravelTime = time;
                return;
            }
        }
        public void DelBusStation(int busCode)
        {
            if (busStations.Count <= 2)
                throw new BusLineException("You can't delete the station");
            int num = Search(busCode);
            if (num == -1)
            {
                throw new BusLineException("the station you have entered doesn't exist");
            }
            if (num == busStations.Count - 1)
            {
                endingPoint = busStations[busStations.Count - 2];
            }
            else if (num == 0)
            {
                startingPoint = busStations[1];
                busStations[1].TravelTime = new TimeSpan(0, 0, 0);
                busStations[1].Distance = 0;
            }
            else
            {
                Console.WriteLine("Enter the travel time between the desired station and the station after it you want to delete");
                TimeSpan time = TimeSpan.Parse(Console.ReadLine());
                Console.WriteLine("Enter the distance between the desired station and the station after it you want to delete");
                double distance = double.Parse(Console.ReadLine());
                busStations[num + 1].Distance = distance;
                busStations[num + 1].TravelTime = time;
            }
            busStations.RemoveAt(num);
        }
        /// <summary>
        /// A Boolean method that checks whether a particular station is on the route of the line
        /// </summary>
        /// <param name="stationCode">The station number tested</param>
        /// <returns>true/false</returns>
        public bool StationExist(int stationCode)
        {
            foreach (BusLineStation item in busStations)
            {
                if(item.BusCode==stationCode)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// A method that check the distance between 2 stations that are on the line The stations are not necessarily adjacent
        /// </summary>
        /// <param name="a">first station</param>
        /// <param name="b">second station</param>
        /// <returns>the distance between the stations</returns>
        public double DistanceBetween(BusLineStation a,BusLineStation b)
        {
            if(!(StationExist(a.BusCode)&&StationExist(b.BusCode)))
            {
                throw new BusLineException("One or more stations you entered do not exist");
            }
            double distanceBetween = 0.0;
            int numA = Search(a.BusCode);
            int numB = Search(b.BusCode);
            for(int i=++numA;i<=numB;i++)
            {
                distanceBetween += busStations[i].Distance;
            }
            return distanceBetween;
        }
        /// <summary>
        /// A method that check the travel time between 2 stations that are on the line The stations are not necessarily adjacent 
        /// </summary>
        /// <param name="a">first station</param>
        /// <param name="b">second station</param>
        /// <returns> the tarvel time between the stations</returns>
        public TimeSpan TimeBetween(BusLineStation a, BusLineStation b)
        {
            if (!(StationExist(a.BusCode) && StationExist(b.BusCode)))
            {
                throw new BusLineException("One or more stations you entered do not exist");
            }
            TimeSpan timeBetween = new TimeSpan(0, 0, 0);
            int numA = Search(a.BusCode);
            int numB = Search(b.BusCode);
            for (int i = ++numA; i <= numB; i++)
            {
                timeBetween = busStations[i].TravelTime;
            }
            return timeBetween;
        }
        /// <summary>
        /// A method that check a sub-trajectory of the line
        /// </summary>
        /// <param name="a">number of first station</param>
        /// <param name="b">number of second station</param>
        /// <returns>A bus line object that actually represented the line segment between the two stations</returns>
        public BusLine SubRoute(int a,int b)
        {
            if (!(StationExist(a) && StationExist(b)))
            {
                throw new BusLineException("One or more stations you entered do not exist");
            }
            List<BusLineStation> route = new List<BusLineStation>();
            int numA = Search(a);
            int numB = Search(b);
            if (numA >= numB)
            {
                throw new BusStationException("there is no route");
            }
            for (int i = numA; i <= numB; i++)
            {
                route.Add(busStations[i]);
            }
            return new BusLine(BusNumber, route, area);
        }
        public int CompareTo(BusLine obj)
        {
            TimeSpan bus1 = TimeBetween(this.StartingPoint, this.EndingPoint);
            TimeSpan bus2 = TimeBetween(this.StartingPoint, this.EndingPoint);
            return bus1.CompareTo(bus2);
        }
    }
}
