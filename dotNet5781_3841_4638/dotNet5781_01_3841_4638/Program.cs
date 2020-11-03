using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace dotNet5781_01_3841_4638
{
    enum choice { insert = 1, select, refuel, print};
    public static class Program
    {
        public static List<Bus> list;
        public static Random r;
        public static void Main(string[] args)
        {
            list = new List<Bus>();
            r = new Random(DateTime.Now.Millisecond);
            while (true)
            {
                Console.WriteLine("Select a number from the following menu:");
                Console.WriteLine("1 To add a bus");
                Console.WriteLine("2 To select a bus from the bus list");
                Console.WriteLine("3 To do maintenance or refueling for the bus");
                Console.WriteLine("4 To view the mileage and license number of the company's vehicles");
                Console.WriteLine("Any other number to exit");
                choice c = (choice)Enum.Parse(typeof(choice), Console.ReadLine());

                switch (c)
                {
                    case choice.insert:
                        insertBus(list);
                        break;
                    case choice.select:
                        selectBus(list);
                        break;
                    case choice.refuel:
                        refuelOrTreat(list);
                        break;
                    case choice.print:
                        printDetails(list);
                        break;
                    default:
                        return;
                        
                }
            }
        }
        public static bool checkNumAndDate(string num,DateTime date)//Checks if the date matches the license number
        {
            if(num.Length<7||num.Length>8)
            {
                Console.WriteLine("The license number you entered is incorrect");
                return false;
            }
            if((date.Year<2018&&num.Length!=7)||(date.Year >= 2018 && num.Length != 8))
            {
                Console.WriteLine("The license number you entered does not match the dateThe number you entered is incorrect");
                return false;
            }
            Console.WriteLine("The license number matches the date you entered");
            return true;
        }
        public static void insertBus(List<Bus> buses)
        {
            double totalKm;
            DateTime startDate = new DateTime();
            Console.WriteLine("Enter your license number:");
            string licenseNumber = Console.ReadLine();
            bool checkDate = false;
            while (checkDate == false)
            {
                Console.WriteLine("Enter your activity start date according to this format dd/MM/yyyy:");
                string startDatestr = Console.ReadLine();
                checkDate = DateTime.TryParseExact(startDatestr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out startDate);
            }
            
           if ( checkNumAndDate(licenseNumber, startDate))
            {
                if (Bus.check(licenseNumber))
                {
                    Console.WriteLine("Enter how many km you have traveled so far");
                    string totalKmstr= Console.ReadLine();
                    bool total = double.TryParse(totalKmstr, out totalKm);
                    Bus bus = new Bus();
                    bus.licenseNumber = licenseNumber;
                    bus.StartDate = startDate;
                    bus.TotalKm = totalKm;
                    bus.fuel = 0;
                    bus.lastKmTreat = 0;
                    bus.lastTreatment = startDate;
                    buses.Add(bus);
                    Console.WriteLine("The data was successfully absorbed");
                }
            }
        }
        public static void selectBus(List<Bus>buses)
        {
            Console.WriteLine("Enter your license number:");
            string licenseNumber = System.Console.ReadLine();
            if (!Bus.check(licenseNumber))
            {
                Console.WriteLine("The license number you entered is incorrect");
                return;
            }
            bool exist = false;
            foreach (Bus bus in buses)
            {
                if (bus.licenseNumber == licenseNumber && exist == false)
                {
                    exist = true;
                }
                Console.Write("The number of kilometers in the requested trip are:");
                double km = r.Next(0, 1201);
                Console.Write(km);
                if (bus.CanTravel(km))
                {
                    bus.TotalKm += km;
                    bus.fuel -= km;
                    Console.WriteLine("You can make the trip");
                    return;
                }
                else
                    return;
            }
                if (!exist)
                {
                    Console.WriteLine(" There is no bus with the license number you entered");
                }
        }
        public static void refuelOrTreat(List<Bus> buses) //The user selects the desired treatment
        {
            Console.WriteLine("Enter your license number");
            String licenseNumber = Console.ReadLine();
            bool c = false;
            foreach (Bus bus in buses)
            {
                if (bus.licenseNumber == licenseNumber && c == false)
                {
                    c = true;
                    Console.WriteLine(" For a treatment enter 't',for refuel enter 'r',");
                    Char ans = Console.ReadLine()[0];
                    while (ans != 'r' && ans != 't')
                    {
                        Console.WriteLine(" Wrong answer");
                        ans = Console.ReadLine()[0];
                    }
                    if (ans == 't')
                    {
                        bus.Treatment();
                        Console.WriteLine("The treatment was performed successfully");
                    }
                    if (ans == 'r')
                    {
                        bus.Refuel();
                        Console.WriteLine("The fuel tank was successfully filled");
                    }
                }
            }
            if (c == false)
                Console.WriteLine(" There is no bus with the license number you entered");
        }
        public static void printDetails(List<Bus> buses)
        {
            foreach (Bus bus in buses)
            {
                Console.WriteLine(bus);
            }
        }
        

    }
}
