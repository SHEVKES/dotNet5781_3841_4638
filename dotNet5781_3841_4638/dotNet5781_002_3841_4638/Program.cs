//Ayala Cohen 212224638
//Shira Shevkes 212433841
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections;

namespace dotNet5781_002_3841_4638
{
   class Program
    {
        static void Main(string[] args)
        {
            List<BusStation> stations = new List<BusStation>();
            List<BusLine> lines = new List<BusLine>();
            BusLineCollection busLinesCollection = new BusLineCollection(lines);
            BuildLines.createLists(ref stations, busLinesCollection);
            Menu answer;
            do
            {
                Console.WriteLine("Choose number between 0-4 ");
                Console.WriteLine("0 for add");
                Console.WriteLine("1 for delete");
                Console.WriteLine("2 for search");
                Console.WriteLine("3 for print");
                Console.WriteLine("4 for exit");
                char ch;
                while (!Menu.TryParse(Console.ReadLine(), out answer))
                    Console.WriteLine("ERROR, enter your answer again");

                switch (answer)
                {
                    case Menu.Add:
                        Console.WriteLine("enter b for adding a new bus line");
                        Console.WriteLine("enter s for adding a new station to an existing bus line");
                        ch = Console.ReadLine()[0];
                        try
                        {
                            if (ch == 'b')
                            {
                                AddingBus(busLinesCollection, stations);
                            }
                            else if (ch == 's')
                            {
                                AddingStation(busLinesCollection, stations);
                            }
                            else
                                Console.WriteLine("ERROR");
                        }
                        catch (FormatException) { Console.WriteLine("Your choice is wrong"); }
                        catch (BusLineException ex) { Console.WriteLine(ex.Message); }
                        catch (BusStationException ex) { Console.WriteLine(ex.Message); }
                        break;
                    case Menu.Delete:
                        Console.WriteLine("enter b for deleting a bus line");
                        Console.WriteLine("enter s for deleting a station of an existing bus line");
                        ch = Console.ReadLine()[0];
                        try
                        {
                            if (ch == 'b')
                                DeleteBus(busLinesCollection);
                            else if (ch == 's')
                                DeleteStation(busLinesCollection);
                            else
                                Console.WriteLine("ERROR");
                        }
                        catch (BusLineException ex) { Console.WriteLine(ex.Message); }
                        catch (FormatException) { Console.WriteLine("Your choice is wrong"); }
                        break;
                    case Menu.Search:
                        Console.WriteLine("enter b for searching a bus lines in a station");
                        Console.WriteLine("enter m for searching buses with the same route");
                        ch = Console.ReadLine()[0];
                        try
                        {
                            if (ch == 'b')
                                PassingInStation(busLinesCollection);
                            else if (ch == 'm')
                                PrintOptions(busLinesCollection);
                            else
                                Console.WriteLine("ERROR");

                        }
                        catch (FormatException) { Console.WriteLine("Your choice is wrong"); }
                        catch (BusStationException ex) { Console.WriteLine(ex.Message); }
                        break;
                    case Menu.Print:
                        Console.WriteLine("enter b for printing all the bus lines");
                        Console.WriteLine("enter s for printing a list of all stations and line numbers passing through them");
                        ch = Console.ReadLine()[0];
                        if (ch == 'b')
                            PrintAllLines(busLinesCollection);
                        if (ch == 's')
                            PrintAllStations(stations, busLinesCollection);
                        break;
                    case Menu.Exit:
                        Console.WriteLine("Good Bye");
                        break;
                    default:
                        break;
                }
            } while (answer != Menu.Exit);
        }
        /// <summary>
        /// The function adds a new bus to the system from a collection of bus lines
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        /// <param name="stations">The stations of the line</param>
        public static void AddingBus(BusLineCollection collection, List<BusStation> stations)
        {
            Console.WriteLine("Please enter the bus line number");
            int lineNum = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the codes of stations, for ending enter -1");
            int busCode = int.Parse(Console.ReadLine());
            List<BusLineStation> route = new List<BusLineStation>();
            while (busCode != -1)
            {
                int index = StationIndex(stations, busCode);
                if (index == -1)
                    throw new BusStationException("ERROR,there is no such station");
                TimeSpan time = new TimeSpan(0, 0, 0);//the time from the previous station
                if (route.Count != 0)
                {
                    Console.WriteLine("Please enter travel time from the previous station.");
                    while (!TimeSpan.TryParse(Console.ReadLine(), out time))
                    {
                        Console.WriteLine("ERROR,enter again");
                    }
                }
                BusStation stopTmp = stations[index];
                BusLineStation stop = new BusLineStation(stopTmp, time);//bulid new bus line station
                if (route.Count == 0)
                    stop.Distance = 0;
                route.Add(stop);
                Console.WriteLine("Please enter the codes of stations, for ending enter -1");
                busCode = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Please enter the area of the bus line route");
            Console.WriteLine("1:General 2:North 3:South 4:Center 5:Jerusalem ");
            Areas area;
            while (!Areas.TryParse(Console.ReadLine(), out area))
            {
                Console.WriteLine("ERROR, enter area again");
            }
            collection.AddLineToCollection(new BusLine(lineNum, route, area));
        }
        public static int StationIndex(List<BusStation> list, int busCode)
        {
            int x = 0;
            foreach (BusStation s in list)
            {
                if (s.BusCode == busCode)
                    return x;
                x++;
            }
            return -1;
        }
        /// <summary>
        /// A function that adds a station to a bus line 
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        /// <param name="stations">The stations of the line</param>
        public static void AddingStation(BusLineCollection collection, List<BusStation> stations)
        {
            Console.WriteLine("Please enter bus number, starting point and ending point");
            int BusNumber = int.Parse(Console.ReadLine());
            int StartingPoint = int.Parse(Console.ReadLine());
            int EndingPoint = int.Parse(Console.ReadLine());
            foreach (BusLine item in collection)
            {
                if (item.BusNumber == BusNumber && item.StartingPoint.BusCode == StartingPoint && item.EndingPoint.BusCode == EndingPoint)
                {
                    Console.WriteLine(" please enter the number of the new station");
                    int code = int.Parse(Console.ReadLine());
                    int index = StationIndex(stations, code);
                    if (index == -1)
                        throw new BusStationException("the station does not exist");
                    if (item.StationExist(code))
                        throw new BusLineException("the station is already exists in this bus line");
                    Console.WriteLine("enter 0: for adding in the begining, 1: for adding in middle, 2: for adding in the end");
                    Insert location;
                    while (!Insert.TryParse(Console.ReadLine(), out location))
                    {
                        Console.WriteLine("ERROR, enter your answer again");
                    }
                    TimeSpan time = new TimeSpan(0, 0, 0);
                    if (location != Insert.First)
                    {
                        Console.WriteLine("Please enter the travel time from a previous station");
                        time = TimeSpan.Parse(Console.ReadLine());
                    }
                    BusStation tmp = stations[index];
                    BusLineStation stop = new BusLineStation(tmp, time);
                    item.AddStation(stop, location);
                    return;
                }
            }
            throw new BusLineException("ERROR,The bus line doesn't exist");
        }
        /// <summary>
        /// A function that deletes a bus line from a collection of bus lines
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        public static void DeleteBus(BusLineCollection collection)
        {
            Console.WriteLine("Please enter bus number, starting point and ending point");
            int busNumber = int.Parse(Console.ReadLine());
            int startingPoint = int.Parse(Console.ReadLine());
            int endingPoint = int.Parse(Console.ReadLine());
            foreach (BusLine item in collection)
            {
                if (item.BusNumber == busNumber && item.StartingPoint.BusCode == startingPoint && item.EndingPoint.BusCode == endingPoint)
                {
                    collection.DeleteBus(item);
                    return;
                }
            }
            throw new BusLineException("ERROR,The bus line doesn't exist");
        }
        /// <summary>
        /// A function that deletes a station from a bus line route
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        public static void DeleteStation(BusLineCollection collection)
        {
            Console.WriteLine("Please enter bus number, starting point and ending point");
            int busNumber = int.Parse(Console.ReadLine());
            int startingPoint = int.Parse(Console.ReadLine());
            int endingPoint = int.Parse(Console.ReadLine());
            foreach (BusLine item in collection)
            {
                if (item.BusNumber == busNumber && item.StartingPoint.BusCode == startingPoint && item.EndingPoint.BusCode == endingPoint)
                {
                    Console.WriteLine("enter the station number you want to delete");
                    int busCode = int.Parse(Console.ReadLine());
                    item.DelBusStation(busCode);
                    return;
                }
            }
            throw new BusLineException("ERROR,The bus line doesn't exist");
        }
        /// <summary>
        /// Function for searching for lines that pass through the station according to the station number
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        public static void PassingInStation(BusLineCollection collection)
        {
            Console.WriteLine("enter the number of the station");
            int stationCode = int.Parse(Console.ReadLine());
            List<BusLine> busLine = collection.LinesPass(stationCode);
            foreach (BusLine item in busLine)
            {
                Console.WriteLine(item.BusNumber);
            }
        }
        /// <summary>
        /// Function for printing the options for travel between 2 stations, without changing buses
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        public static void PrintOptions(BusLineCollection collection)
        {
            Console.WriteLine("enter the number of your starting station");
            int startStat = int.Parse(Console.ReadLine());
            Console.WriteLine("enter the number of your ending station");
            int endStat = int.Parse(Console.ReadLine());
            List<BusLine> busLines = new List<BusLine>();
            foreach (BusLine item in collection)
            {
                try
                {
                    BusLine busRoute = item.SubRoute(startStat, endStat);
                    busLines.Add(busRoute);
                }
                catch (BusLineException) { }
            }
            if (busLines.Count == 0)
                throw new BusLineException("The line does not match");
            BusLineCollection sortedByTime = new BusLineCollection(busLines);
            busLines = sortedByTime.SortedLineList();
            Console.WriteLine("The buses by their travel time:");
            foreach (BusLine item in busLines)
            {
                Console.WriteLine(item.BusNumber);
            }
        }
        /// <summary>
        /// A function that prints all bus lines in the system
        /// </summary>
        /// <param name="collection">A collection of buses</param>
        public static void PrintAllLines(BusLineCollection collection)
        {
            foreach (BusLine item in collection)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// A function that prints the list of all stations and line numbers passing through them
        /// </summary>
        /// <param name="stations">The stations of the line</param>
        /// <param name="collection">A collection of buses</param>
        public static void PrintAllStations(List<BusStation> stations, BusLineCollection collection)
        {
            foreach (BusStation item in stations)
            {
                Console.WriteLine("In station " + item + ":");
                foreach (BusLine bus in collection)
                {
                    if (bus.StationExist(item.BusCode))
                        Console.WriteLine(bus.BusNumber);
                }
            }
        }
    }
}

