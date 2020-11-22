using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace dotNet5781_002_3841_4638
{
    class BusLineCollection : IEnumerable
    {
       public List<BusLine> Lines { get; set; }
       public BusLineCollection(List<BusLine> list)
        {
            Lines = new List<BusLine>(list);
        }
        public IEnumerator GetEnumerator()
        {
            return Lines.GetEnumerator();
        }
        public List<BusLine> this[int busNumber]
        {
            get
            {
                List<BusLine> bsl = Lines.FindAll(item => item.BusNumber == busNumber);
                if (bsl.Count != 0)
                    return bsl;
                else
                    throw new BusLineException("There is no buses with this line number");
            }
        }
        public int StationCounter( BusLine bus)
        {
            int x = 0;
            foreach (BusLine item in Lines)
            {
                if(item.BusNumber==bus.BusNumber)
                {
                    x++;
                }
            }
            return x;
        }
        /// <summary>
        /// Checks if the buses are equal - are they the same bus
        /// </summary>
        /// <param name="bus">bus line</param>
        /// <returns>the bus number</returns>
        public int BusIndex(BusLine bus) 
        {
            int index = 0;
            foreach (BusLine item in Lines)
            {
                if (item.BusNumber == bus.BusNumber && item.StartingPoint == bus.StartingPoint && item.EndingPoint == bus.EndingPoint)
                    return index;
                index++;
            }
            return -1;
        }
        public bool IsExist(List<BusLineStation> list)
        {
            foreach (BusLine item in Lines)
            {
                int i;
                for (i = 0; i < list.Count && i < item.busStations.Count; i++)
                {
                    if (list[i].BusCode != item.busStations[i].BusCode)
                        break;
                }
                if ((i == list.Count) || (i == item.busStations.Count))
                    return true;
            }
            return false;
        }

        public void AddLineToCollection(BusLine bus)
        {
            if (IsExist(bus.busStations))
                throw new BusLineException("The line used");
            int count = StationCounter(bus);
            if (count == 2)
                throw new BusLineException("This bus line number is already exists");
            else 
            {
                Lines.Add(bus);
                return;
            }
        }
        
        public void DeleteBus(BusLine bus)
        {
            int index = BusIndex(bus);
            if (index == -1)
                throw new BusLineException("ERROR,the bus does not exist");
            Lines.RemoveAt(index);
        }
        /// <summary>
        /// A method that receives a bus station number and returns the list of lines passing through that station
        /// </summary>
        /// <param name="stationCode">number of station</param>
        /// <returns>list of lines</returns>
        public List<BusLine> LinesPass(int stationCode)
        {
            List<BusLine> list = new List<BusLine>();
            foreach (BusLine item in Lines)
            {
                if (item.StationExist(stationCode))
                    list.Add(item);
            }
            if (list.Count == 0)
                throw new BusStationException("The bus does not exist at the current station");
            return list;
        }
        /// <summary>
        /// A method that sorts all the lines according to the total travel time, from short to long
        /// </summary>
        /// <returns>the sorted list of lines</returns>
        public List<BusLine> SortedLineList()
        {
            BusLine[] busLineArry = new BusLine[Lines.Count];
            Lines.CopyTo(busLineArry);
            List<BusLine> list = busLineArry.ToList();
            list.Sort();
            return list;
        }
    }
}
