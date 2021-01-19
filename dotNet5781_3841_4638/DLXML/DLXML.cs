using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;

namespace DL
{
    sealed class DLXML : IDL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML()
        {
            //if (!File.Exists(stationsPath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.Station>(DS.DataSource.ListStations, stationsPath);

            ////if (!File.Exists(linesPath))
            ////    DL.XMLTools.SaveListToXMLSerializer<DO.Line>(DS.DataSource.ListLines, linesPath);

            //if (!File.Exists(adjacentStationsPath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.AdjacentStations>(DS.DataSource.ListAdjacentStations, adjacentStationsPath);

            ////if (!File.Exists(lineStationsPath))
            ////    DL.XMLTools.SaveListToXMLSerializer<DO.LineStation>(DS.DataSource.ListLineStations, lineStationsPath);

            //if (!File.Exists(busesPath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.Bus>(DS.DataSource.ListBuses, busesPath);

            //if (!File.Exists(usersPath))
            //    DL.XMLTools.SaveListToXMLSerializer<DO.User>(DS.DataSource.ListUsers, usersPath);

        } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        string lineTripsPath = @"LineTripsXml.xml"; //XElement
        string busesPath = @"BusesXml.xml"; //XMLSerializer
        string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string linesPath = @"LinesXml.xml"; //XMLSerializer
        string lineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string usersPath = @"UsersXml.xml"; //XMLSerializer
        string runningNumberPath = @"RunningNumberPathXml.xml"; //XMLSerializer
        #endregion

        #region User
        public IEnumerable<DO.User> GetAllUsers()
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            return (from item in usersRootElem.Elements()
                    where bool.Parse(item.Element("IsDeleted").Value) == false
                    select new User()
                    {
                        UserName = item.Element("UserName").Value,
                        Password = item.Element("Password").Value,
                        FirstName = item.Element("FirstName").Value,
                        LastName=item.Element("LastName").Value,
                        AdminAccess = bool.Parse(item.Element("AdminAccess").Value),
                        IsDeleted = bool.Parse(item.Element("IsDeleted").Value)
                    }
                   );
        }
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            return from item in usersRootElem.Elements()
                   let u1 = new User()
                   {
                       UserName = item.Element("UserName").Value,
                       Password = item.Element("Password").Value,
                       FirstName = item.Element("FirstName").Value,
                       LastName = item.Element("LastName").Value,
                       AdminAccess = bool.Parse(item.Element("AdminAccess").Value),
                       IsDeleted = bool.Parse(item.Element("IsDeleted").Value)
                   }
                   where predicate(u1)
                   select u1;
        }
        public DO.User GetUser(string userName)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);
            User user = (from item in usersRootElem.Elements()
                         where item.Element("UserName").Value == userName && bool.Parse(item.Element("IsDeleted").Value) == false
                         select new User()
                         {
                             UserName = item.Element("UserName").Value,
                             Password = item.Element("Password").Value,
                             FirstName = item.Element("FirstName").Value,
                             LastName = item.Element("LastName").Value,
                             AdminAccess = bool.Parse(item.Element("AdminAccess").Value),
                             IsDeleted = bool.Parse(item.Element("IsDeleted").Value)
                         }
                       ).FirstOrDefault();

            if (user == null)
                throw new DO.BadUserNameException(userName, "שם המשתמש שהיקשת אינו קיים במערכת");
            return user;
        }
        public void AddUser(DO.User user)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(usersPath);

            XElement user1 = (from u in usersRootElem.Elements()
                              where u.Element("UserName").Value == user.UserName && bool.Parse(u.Element("IsDeleted").Value) == false
                              select u).FirstOrDefault();

            if (user1 != null)
                throw new BadUserNameException(user.UserName, "שם המשתמש שהיקשת קיים כבר במערכת");

            XElement userElem = new XElement("User",
                                   new XElement("UserName", user.UserName),
                                   new XElement("FirstName", user.FirstName),
                                   new XElement("LastName", user.LastName),
                                   new XElement("Password", user.Password),
                                   new XElement("AdminAccess", user.AdminAccess.ToString()),
                                   new XElement("IsDeleted", user.IsDeleted.ToString()));
            usersRootElem.Add(userElem);
            XMLTools.SaveListToXMLElement(usersRootElem, usersPath);
        }
        public void UpdateUser(DO.User user)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement userElem = (from u in usersRootElem.Elements()
                                 where u.Element("UserName").Value == user.UserName && bool.Parse(u.Element("IsDeleted").Value) == false
                                 select u).FirstOrDefault();

            if (userElem != null)
            {
                userElem.Element("UserName").Value = user.UserName;
                userElem.Element("FirstName").Value = user.FirstName;
                userElem.Element("LastName").Value = user.LastName;
                userElem.Element("Password").Value = user.Password;
                userElem.Element("AdminAccess").Value = user.AdminAccess.ToString();
                userElem.Element("IsDeleted").Value = user.IsDeleted.ToString();
                XMLTools.SaveListToXMLElement(usersRootElem, lineTripsPath);
            }
            else
                throw new BadUserNameException(user.UserName, "שם המשתמש שהיקשת אינו קיים במערכת");
        }
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            throw new NotImplementedException();
            //DO.User userFind = DataSource.ListUsers.Find(u => u.UserName == userName && u.IsDeleted == false);
            //if (userFind == null)
            //    throw new Exception();
            //update(userFind);
        }
        public void DeleteUser(string userName)
        {
            XElement usersRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);
            XElement user = (from u in usersRootElem.Elements()
                             where u.Element("UserName").Value == userName && bool.Parse(u.Element("IsDeleted").Value) == false
                             select u).FirstOrDefault();

            if (user != null)
            {
                user.Element("IsDeleted").Value = true.ToString();
                XMLTools.SaveListToXMLElement(usersRootElem, lineTripsPath);
            }
            else
                throw new BadUserNameException(userName, "שם המשתמש שהיקשת אינו קיים במערכת");
        }

        #endregion

        #region Bus
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            return from item in ListBuses
                   where item.IsDeleted == false
                   select item;
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            return from item in ListBuses
                   where predicate(item)
                   select item;
        }
        public DO.Bus GetBus(int licenseNum)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus bus = ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if (bus != null)
                return bus;
            else
                throw new DO.BadLicenseNumException(licenseNum, "מספר הרישוי שהזנת אינו קיים במערכת");
        }
        public void AddBus(DO.Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            if (ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false) != null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "האוטובוס הינו קיים כבר במערכת");
            ListBuses.Add(bus);
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        public void UpdateBus(DO.Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus busFind = ListBuses.Find(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false);
            if (busFind == null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, " האוטובוס  שהזנת אינו קיים במערכת");
            DO.Bus bus1 = bus; //copy the bus to a new item
            ListBuses.Remove(busFind); //delete the bus from the list
            ListBuses.Add(bus1); //add the new bus to the list of buses      
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)
        {
            //List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            //DO.Bus busFind = ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            //if (busFind == null)
            //    throw new BadLicenseNumException(licenseNum, "The bus does not exist");
            //update(busFind);
        }
        public void DeleteBus(int licenseNum)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus bus = ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if (bus == null) //The bus has already been deleted or does not exist
                throw new DO.BadLicenseNumException(licenseNum, "מספר הרישוי שהזנת אינו קיים במערכת");
            bus.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        #endregion

        #region AdjacentStations
        public bool IsExistAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adj = (from a in adjStationssRootElem.Elements()
                            where int.Parse(a.Element("StationCode1").Value) == stationCode1 && int.Parse(a.Element("StationCode2").Value) == stationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                            select a).FirstOrDefault();
            if (adj == null)
                return false;
            return true;
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return (from a in adjStationssRootElem.Elements()
                    where bool.Parse(a.Element("IsDeleted").Value) == false
                    select new AdjacentStations()
                    {
                        StationCode1 = Int32.Parse(a.Element("StationCode1").Value),
                        StationCode2 = Int32.Parse(a.Element("StationCode2").Value),
                        Distance = double.Parse(a.Element("Distance").Value),
                        Time = TimeSpan.Parse(a.Element("Time").Value),
                        IsDeleted = Convert.ToBoolean(a.Element("IsDeleted").Value)
                    }
                   );
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            XElement adjStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return from a in adjStationsRootElem.Elements()
                   let a1 = new AdjacentStations()
                   {
                       StationCode1 = Int32.Parse(a.Element("StationCode1").Value),
                       StationCode2 = Int32.Parse(a.Element("StationCode2").Value),
                       Distance = double.Parse(a.Element("Distance").Value),
                       Time = TimeSpan.Parse(a.Element("Time").Value),
                       IsDeleted = Convert.ToBoolean(a.Element("IsDeleted").Value)
                   }
                   where predicate(a1)
                   select a1;
        }
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            AdjacentStations adj = (from a in adjStationssRootElem.Elements()
                                    where int.Parse(a.Element("StationCode1").Value) == stationCode1 && int.Parse(a.Element("StationCode2").Value) == stationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                                    select new AdjacentStations()
                                    {
                                        StationCode1 = Int32.Parse(a.Element("StationCode1").Value),
                                        StationCode2 = Int32.Parse(a.Element("StationCode2").Value),
                                        Distance = double.Parse(a.Element("Distance").Value),
                                        Time = TimeSpan.Parse(a.Element("Time").Value),
                                        IsDeleted = Convert.ToBoolean(a.Element("IsDeleted").Value)
                                    }
                        ).FirstOrDefault();

            if (adj == null)
                throw new DO.BadAdjacentStationsException(stationCode1, stationCode2, "התחנות העוקבות אינן קיימות במערכת");
            return adj;
        }
        public void AddAdjacentStations(DO.AdjacentStations adjStations)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adj1 = (from a in adjStationssRootElem.Elements()
                             where int.Parse(a.Element("StationCode1").Value) == adjStations.StationCode1 && int.Parse(a.Element("StationCode2").Value) == adjStations.StationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                             select a).FirstOrDefault();

            if (adj1 != null)
                throw new BadAdjacentStationsException(adjStations.StationCode1, adjStations.StationCode2, "התחנות העוקבות שהקשת קיימות כבר במערכת"); ;

            XElement adjElem = new XElement("AdjacentStations",
                                   new XElement("StationCode1", adjStations.StationCode1.ToString()),
                                   new XElement("StationCode2", adjStations.StationCode2.ToString()),
                                   new XElement("Distance", adjStations.Distance.ToString()),
                                   new XElement("Time", adjStations.Time.ToString()),
                                   new XElement("IsDeleted", adjStations.IsDeleted.ToString()));
            adjStationssRootElem.Add(adjElem);
            XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjStations)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);
            XElement adj = (from a in adjStationssRootElem.Elements()
                            where int.Parse(a.Element("StationCode1").Value) == adjStations.StationCode1 && int.Parse(a.Element("StationCode2").Value) == adjStations.StationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                            select a).FirstOrDefault();

            if (adj != null)
            {
                adj.Element("StationCode1").Value = adjStations.StationCode1.ToString();
                adj.Element("StationCode2").Value = adjStations.StationCode2.ToString();
                adj.Element("Distance").Value = adjStations.Distance.ToString();
                adj.Element("Time").Value = adjStations.Time.ToString();
                adj.Element("IsDeleted").Value = adjStations.IsDeleted.ToString();

                XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
            }
            else
                throw new BadAdjacentStationsException(adjStations.StationCode1, adjStations.StationCode2, "התחנות העוקבות אינן קיימות במערכת");
        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            throw new NotImplementedException();
            //DO.AdjacentStations adjFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false));
            //if (adjFind == null)
            //    throw new BadAdjacentStationsException(stationCode1, stationCode2, "The adjacent stations does not exist");
            //update(adjFind);
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adj = (from a in adjStationssRootElem.Elements()
                            where int.Parse(a.Element("StationCode1").Value) == stationCode1 && int.Parse(a.Element("StationCode2").Value) == stationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                            select a).FirstOrDefault();

            if (adj != null)
            {
                adj.Element("IsDeleted").Value = true.ToString();
                XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
            }
            else
                throw new BadAdjacentStationsException(stationCode1, stationCode2, "התחנות העוקבות אינן קיימות במערכת");
        }
        #endregion

        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from line in ListLines
                   where line.IsDeleted == false
                   select line;

        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from line in ListLines
                   where predicate(line)
                   select line;

        }
        public DO.Line GetLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            DO.Line line = ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (line != null)
                return line;
            else
                throw new BadLineIdException(lineId, "מזהה הקו אינו קיים במערכת");
        }
        public void AddLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            line.LineId = XMLTools.GetRunningNumber(runningNumberPath);
            if (ListLines.FirstOrDefault(l => l.LineId == line.LineId && l.IsDeleted == false) != null)
                throw new BadLineIdException(line.LineId, "מזהה הקו הינו קיים כבר במערכת");
            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }
        public void UpdateLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            DO.Line lineFind = ListLines.Find(l => l.LineId == line.LineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(line.LineId, "מזהה הקו אינו קיים במערכת");
            ListLines.Remove(lineFind);
            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            throw new NotImplementedException();
            //DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            //if (lineFind == null)
            //    throw new BadLineIdException(lineId, "The Line ID does not exist");
            //update(lineFind);
        }
        public void DeleteLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            DO.Line lineFind = ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "מזהה הקו אינו קיים במערכת");
            lineFind.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }

        #endregion

        #region LineStation
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from lStat in ListLineStations
                   where lStat.IsDeleted == false
                   select lStat;
        }
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from lStat in ListLineStations
                   where predicate(lStat)
                   select lStat;
        }
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lineStation = ListLineStations.Find(lStat => (lStat.LineId == lineId && lStat.StationCode == stationCode && lStat.IsDeleted == false));
            if (lineStation != null)
                return lineStation;
            else
                throw new DO.BadLineStationException(lineId, stationCode, "Tתחנת הקו שהקשת אינה קיימת במערכת");
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            if (ListLineStations.FirstOrDefault(lStat => (lStat.LineId == lineStation.LineId && lStat.StationCode == lineStation.StationCode && lStat.IsDeleted == false)) != null)//if this line station already exists in the list
                throw new DO.BadLineStationException(lineStation.LineId, lineStation.StationCode, "תחנת הקו שהקשת קיימת כבר במערכת");
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lStatFind = ListLineStations.Find(lStat => (lStat.LineId == lineStation.LineId && lStat.StationCode == lineStation.StationCode && lStat.IsDeleted == false));
            if (lStatFind == null)
                throw new DO.BadLineStationException(lineStation.LineId, lineStation.StationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
            ListLineStations.Remove(lStatFind);
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            throw new NotImplementedException();
            //DO.LineStation lStatFind = DataSource.ListLineStations.Find(lStat => (lStat.LineId == lineId && lStat.StationCode == stationCode && lStat.IsDeleted == false));
            //if (lStatFind == null)
            //    throw new DO.BadLineStationException(lineId, stationCode, "The station line does not exist");
            //update(lStatFind);
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lStatFind = ListLineStations.Find(lStat => (lStat.LineId == lineId && lStat.StationCode == stationCode && lStat.IsDeleted == false));
            if (lStatFind == null)
                throw new DO.BadLineStationException(lineId, stationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
            lStatFind.IsDeleted = true;//delete
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);

        }



        #endregion

        #region LineTrip
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return (from lt in lineTripsRootElem.Elements()
                    where bool.Parse(lt.Element("IsDeleted").Value) == false
                    select new LineTrip()
                    {
                        LineId = int.Parse(lt.Element("LineId").Value),
                        StartAt = TimeSpan.Parse(lt.Element("StartAt").Value),
                        IsDeleted = bool.Parse(lt.Element("IsDeleted").Value)
                    }
                   );
        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return from lt in lineTripsRootElem.Elements()
                   let lt1 = new LineTrip()
                   {
                       LineId = int.Parse(lt.Element("LineId").Value),
                       StartAt = TimeSpan.Parse(lt.Element("StartAt").Value),
                       IsDeleted = bool.Parse(lt.Element("IsDeleted").Value)
                   }
                   where predicate(lt1)
                   select lt1;
        }
        public DO.LineTrip GetLineTrip(int lineId, TimeSpan time)
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);
            LineTrip lineTrip = (from lt in lineTripsRootElem.Elements()
                                 where int.Parse(lt.Element("LineId").Value) == lineId && TimeSpan.Parse(lt.Element("StartAt").Value) == time && bool.Parse(lt.Element("IsDeleted").Value) == false
                                 select new LineTrip()
                                 {
                                     LineId = int.Parse(lt.Element("LineId").Value),
                                     StartAt = TimeSpan.Parse(lt.Element("StartAt").Value),
                                     IsDeleted = bool.Parse(lt.Element("IsDeleted").Value)
                                 }
                       ).FirstOrDefault();

            if (lineTrip == null)
                throw new DO.BadLineTripException(lineId, time, "נסיעת הקו לא קיימת במערכת");
            return lineTrip;
        }
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement lineTrip1 = (from lt in lineTripsRootElem.Elements()
                                  where int.Parse(lt.Element("LineId").Value) == lineTrip.LineId && TimeSpan.Parse(lt.Element("StartAt").Value) == lineTrip.StartAt && bool.Parse(lt.Element("IsDeleted").Value) == false
                                  select lt).FirstOrDefault();

            if (lineTrip1 != null)
                throw new BadLineTripException(lineTrip.LineId, lineTrip.StartAt, "שם המשתמש שהיקשת קיים כבר במערכת");

            XElement lineTripElem = new XElement("LineTrip",
                                    new XElement("LineId", lineTrip.LineId.ToString()),
                                    new XElement("StartAt", lineTrip.StartAt.ToString()),
                                    new XElement("IsDeleted", lineTrip.IsDeleted.ToString()));
            lineTripsRootElem.Add(lineTripElem);
            XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement lineTripElem = (from lt in lineTripsRootElem.Elements()
                                     where int.Parse(lt.Element("LineId").Value) == lineTrip.LineId && TimeSpan.Parse(lt.Element("StartAt").Value) == lineTrip.StartAt && bool.Parse(lt.Element("IsDeleted").Value) == false
                                     select lt).FirstOrDefault();

            if (lineTripElem != null)
            {
                lineTripElem.Element("LineId").Value = lineTrip.LineId.ToString();
                lineTripElem.Element("StartAt").Value = lineTrip.StartAt.ToString();
                lineTripElem.Element("IsDeleted").Value = lineTrip.IsDeleted.ToString();
                XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
            }
            else
                throw new BadLineTripException(lineTrip.LineId, lineTrip.StartAt, "נסיעת הקו לא קיימת במערכת");
        }
        public void UpdateLineTrip(int lineId, TimeSpan time, Action<DO.LineTrip> update)
        {
            throw new NotImplementedException();
            //DO.LineTrip lTripFind = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.StartAt == time && l.IsDeleted == false);
            //if (lTripFind == null)
            //    throw new BadLineTripException(lineId, time, "The line trip does not exist");
            //update(lTripFind);
        }
        public void DeleteLineTrip(int lineId, TimeSpan time)
        {
            XElement lineTripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);
            XElement lineTrip = (from lt in lineTripsRootElem.Elements()
                                 where int.Parse(lt.Element("LineId").Value) == lineId && TimeSpan.Parse(lt.Element("StartAt").Value) == time && bool.Parse(lt.Element("IsDeleted").Value) == false
                                 select lt).FirstOrDefault();

            if (lineTrip != null)
            {
                lineTrip.Element("IsDeleted").Value = true.ToString();
                XMLTools.SaveListToXMLElement(lineTripsRootElem, lineTripsPath);
            }
            else
                throw new BadLineTripException(lineId, time, "נסיעת הקו לא קיימת במערכת");
        }

        #endregion

        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from stat in ListStations
                   where stat.IsDeleted == false
                   select stat;
        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from stat in ListStations
                   where predicate(stat)
                   select stat;
        }
        public DO.Station GetStation(int code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station station = ListStations.Find(s => s.Code == code && s.IsDeleted == false);
            if (station != null)
                return station;
            else
                throw new BadStationCodeException(code, "קוד התחנה שהקשת אינו קיים במערכת");
        }
        public void AddStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (ListStations.FirstOrDefault(s => s.Code == station.Code && s.IsDeleted == false) != null)
                throw new DO.BadStationCodeException(station.Code, "התחנה הינה קיימת כבר במערכת");
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        public void UpdateStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station statFind = ListStations.FirstOrDefault(s => s.Code == station.Code && s.IsDeleted == false);
            if (statFind == null)
                throw new BadStationCodeException(statFind.Code, "קוד התחנה שהקשת אינו קיים במערכת");
            ListStations.Remove(statFind);
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {
            throw new NotImplementedException();
            //DO.Station statFind = DataSource.ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);
            //if (statFind == null)
            //    throw new BadStationCodeException(code, "The station does not exist");
            //update(statFind);
        }
        public void DeleteStation(int code)
        {
            //throw new NotImplementedException();
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station statFind = ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);
            if (statFind == null)
                throw new BadStationCodeException(code, "קוד התחנה שהקשת אינו קיים במערכת");
            statFind.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }

        #endregion

        #region Trip
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return (from t in tripsRootElem.Elements()
                    where bool.Parse(t.Element("IsDeleted").Value) == false
                    select new Trip()
                    {
                        TripId = Int32.Parse(t.Element("TripId").Value),
                        UserName = t.Element("UserName").Value,
                        GetOnStation = Int32.Parse(t.Element("GetOnStation").Value),
                        GetOnTime = TimeSpan.Parse(t.Element("GetOnTime").Value),
                        GetOffStation = Int32.Parse(t.Element("GetOutStation").Value),
                        GetOffTime = TimeSpan.Parse(t.Element("GetOutTime").Value),
                        IsDeleted = bool.Parse(t.Element("IsDeleted").Value)
                    }
                   );
        }
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            return from t in tripsRootElem.Elements()
                   let t1 = new Trip()
                   {
                       TripId = Int32.Parse(t.Element("TripId").Value),
                       UserName = t.Element("UserName").Value,
                       GetOnStation = Int32.Parse(t.Element("GetOnStation").Value),
                       GetOnTime = TimeSpan.Parse(t.Element("GetOnTime").Value),
                       GetOffStation = Int32.Parse(t.Element("GetOutStation").Value),
                       GetOffTime = TimeSpan.Parse(t.Element("GetOutTime").Value),
                       IsDeleted = bool.Parse(t.Element("IsDeleted").Value)
                   }
                   where predicate(t1)
                   select t1;
        }
        public DO.Trip GetTrip(int tripId)
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);
            Trip trip = (from t in tripsRootElem.Elements()
                         where int.Parse(t.Element("TripId").Value) == tripId && bool.Parse(t.Element("IsDeleted").Value) == false
                         select new Trip()
                         {
                             TripId = Int32.Parse(t.Element("TripId").Value),
                             UserName = t.Element("UserName").Value,
                             GetOnStation = Int32.Parse(t.Element("GetOnStation").Value),
                             GetOnTime = TimeSpan.Parse(t.Element("GetOnTime").Value),
                             GetOffStation = Int32.Parse(t.Element("GetOutStation").Value),
                             GetOffTime = TimeSpan.Parse(t.Element("GetOutTime").Value),
                             IsDeleted = bool.Parse(t.Element("IsDeleted").Value)
                         }
                       ).FirstOrDefault();

            if (trip == null)
                throw new DO.BadTripIdException(tripId, "המסלול אינו קיים במערכת");
            return trip;
        }
        public void AddTrip(DO.Trip trip)
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement trip1 = (from t in tripsRootElem.Elements()
                              where int.Parse(t.Element("TripId").Value) == trip.TripId && bool.Parse(t.Element("IsDeleted").Value) == false
                              select t).FirstOrDefault();

            if (trip1 != null)
                throw new BadTripIdException(trip.TripId, "המסלול קיים כבר במערכת");

            XElement tripElem = new XElement("Trip",
                                   new XElement("TripId", trip.TripId.ToString()),
                                   new XElement(" UserName", trip.UserName),
                                   new XElement("GetOnStation", trip.GetOnStation.ToString()),
                                   new XElement("GetOnTime", trip.GetOnTime.ToString()),
                                   new XElement("GetOutStation", trip.GetOffStation.ToString()),
                                   new XElement("GetOutTime", trip.GetOffTime.ToString()),
                                   new XElement("IsDeleted", trip.IsDeleted.ToString())
                                   );
            tripsRootElem.Add(tripElem);
            XMLTools.SaveListToXMLElement(tripsRootElem, lineTripsPath);
        }
        public void UpdateTrip(DO.Trip trip)
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);

            XElement tripElem = (from t in tripsRootElem.Elements()
                                 where int.Parse(t.Element("TripId").Value) == trip.TripId && bool.Parse(t.Element("IsDeleted").Value) == false
                                 select t).FirstOrDefault();

            if (tripElem != null)
            {
                tripElem.Element("TripId").Value = trip.TripId.ToString();
                tripElem.Element(" UserName").Value = trip.UserName;
                tripElem.Element("GetOnStation").Value = trip.GetOnStation.ToString();
                tripElem.Element("GetOnTime").Value = trip.GetOnTime.ToString();
                tripElem.Element("GetOutStation").Value = trip.GetOffStation.ToString();
                tripElem.Element("GetOutTime").Value = trip.GetOffTime.ToString();
                tripElem.Element("IsDeleted").Value = trip.IsDeleted.ToString();
                XMLTools.SaveListToXMLElement(tripsRootElem, lineTripsPath);
            }
            else
                throw new BadTripIdException(trip.TripId, "המסלול אינו קיים במערכת");
        }
        public void UpdateTrip(int tripId, Action<DO.Trip> update)
        {
            throw new NotImplementedException();
            //DO.Trip tripFind = DataSource.ListTrips.Find(t => t.TripId == tripId && t.IsDeleted == false);
            //if (tripFind == null)
            //    throw new Exception();
            //update(tripFind);
        }
        public void DeleteTrip(int tripId)
        {
            XElement tripsRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath);
            XElement trip = (from t in tripsRootElem.Elements()
                             where int.Parse(t.Element("TripId").Value) == tripId && bool.Parse(t.Element("IsDeleted").Value) == false
                             select t).FirstOrDefault();

            if (trip != null)
            {
                trip.Element("IsDeleted").Value = true.ToString();
                XMLTools.SaveListToXMLElement(tripsRootElem, lineTripsPath);
            }
            else
                throw new BadTripIdException(tripId, "שם המשתמש שהקשת אינו קיים במערכת");
        }

        #endregion
    }
}