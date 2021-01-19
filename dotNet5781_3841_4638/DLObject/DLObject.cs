using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
//using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion
        #region Bus
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses
                   where bus.IsDeleted==false
                   select bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        public DO.Bus GetBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted==false);
            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadLicenseNumException(licenseNum,"מספר הרישוי שהזנת אינו קיים במערכת");
        }
        private int LengthLicenseNum(int licenseNum)
        {
            int counter = 0;
            while (licenseNum != 0)
            {
                licenseNum = licenseNum / 10;
                counter++;
            }
            return counter;
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false) != null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "האוטובוס הינו קיים כבר במערכת");
            int length = LengthLicenseNum(bus.LicenseNum);
            if ((length == 7 && bus.FromDate.Year >= 2018) || ((length == 8 && bus.FromDate.Year < 2018)))
                //!(length == 7 && bus.FromDate.Year < 2018) || (length == 8 && bus.FromDate.Year >= 2018))
                throw new DO.BadInputException("מספר הרשוי שהקשת אינו תקין");
            if (bus.FromDate > DateTime.Now)
                throw new DO.BadInputException("תאריך התחלת הפעילות שהקשת אינו תקין");
            if (bus.TotalTrip < 0)
                throw new DO.BadInputException("סך הקילומטרים שהקשת אינו תקין");
            if (bus.FuelRemain < 0 || bus.FuelRemain > 1200)
                throw new DO.BadInputException("כמות הדלק שהקשת אינה תקינה");
            if (bus.DateLastTreat < bus.FromDate || bus.DateLastTreat > DateTime.Now)
                throw new DO.BadInputException("התאריך של הטיפול האחרון שהקשת אינו תקין");
            if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalTrip)
                throw new DO.BadInputException("סך הקילומטרים מאז הטיפול האחרון שהקשת אינו תקין");
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false);
            if (busFind == null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, " האוטובוס  שהזנת אינו קיים במערכת");
            DO.Bus bus1 = bus.Clone(); //copy the bus to a new item
            //busFind.IsDeleted = true; //change the status of the bus to deleted
            DataSource.ListBuses.Remove(busFind); //delete the bus from the list
            DataSource.ListBuses.Add(bus1); //add the new bus to the list of buses                
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if(bus==null)
                throw new DO.BadLicenseNumException(bus.LicenseNum, "האוטובוס שהזנת אינו קיים במערכת");
            update(bus);
        }
        public void DeleteBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if (bus == null) //The bus has already been deleted or does not exist
                throw new DO.BadLicenseNumException(licenseNum, "מספר הרישוי שהזנת אינו קיים במערכת");
            bus.IsDeleted = true;
        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from item in DataSource.ListStations
                   where item.IsDeleted == false
                   select item.Clone();
        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from item in DataSource.ListStations
                   where predicate(item)
                   select item.Clone();
        }
        public DO.Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(s => s.Code == code && s.IsDeleted==false);
            if (station != null)
                return station.Clone();
            else
                throw new DO.BadStationCodeException(code,"קוד התחנה שהקשת אינו קיים במערכת");
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(s => s.Code == station.Code && s.IsDeleted == false) != null)
                throw new DO.BadStationCodeException(station.Code, "התחנה הינה קיימת כבר במערכת");
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station st = DataSource.ListStations.Find(s => s.Code == station.Code && s.IsDeleted == false);
            if (st == null)
                throw new DO.BadStationCodeException(station.Code, "קוד התחנה שהקשת אינו קיים במערכת");     
            DO.Station station1 = station.Clone(); //copy the station to a new item
            st.IsDeleted = true; //change the status of the station to deleted
            DataSource.ListStations.Add(station1); //add the update station to the list
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {
            DO.Station statFind = DataSource.ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);
            if (statFind == null)
                throw new DO.BadStationCodeException(code, "קוד התחנה שהקשת אינו קיים במערכת");
            update(statFind);
        }
        public void DeleteStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(s => s.Code == code && s.IsDeleted == false);
            if(station==null)
                throw new DO.BadStationCodeException(code, "קוד התחנה שהקשת אינו קיים במערכת");
            station.IsDeleted = true;
            foreach (DO.AdjacentStations item in DataSource.ListAdjacentStations)
            {
                if (item.StationCode1 == code || item.StationCode2 == code && item.IsDeleted == false)
                    item.IsDeleted = true;
            }
            //foreach (DO.LineStation item in DataSource.ListLineStations)
            //{
            //    if (item.StationCode == code && item.IsDeleted == false)
            //        item.IsDeleted = true;
            //}
        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from item in DataSource.ListLines
                   where item.IsDeleted == false
                   select item.Clone();
        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            return from item in DataSource.ListLines
                   where predicate(item)
                   select item.Clone();
        }
        public DO.Line GetLine(int lineId)
        {
            DO.Line line = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted==false);
            if (line != null)
                return line.Clone();
            else
                throw new DO.BadLineIdException(lineId, "הקו אינו קיים במערכת");
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(l => l.LineId == line.LineId && l.IsDeleted == false) != null)
                throw new DO.BadLineIdException(line.LineId, "הקו הינו קיים כבר במערכת");
            DataSource.ListLines.Add(line.Clone());           
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line newLine = DataSource.ListLines.Find(l => l.LineId == line.LineId && l.IsDeleted == false);
            if (newLine == null)
                throw new DO.BadLineIdException(line.LineId, "הקו אינו קיים במערכת");
            DO.Line line1 = line.Clone(); //copy the line to a new item
            newLine.IsDeleted = true; //change the status of the line to deleted
            DataSource.ListLines.Add(line1); //add the update line to the list
            //newLine = line1; //update the line
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            DO.Line newLine = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (newLine == null)
                throw new DO.BadLineIdException(lineId, "הקו אינו קיים במערכת");
            update(newLine);
        }
        public void DeleteLine(int lineId)
        {
            DO.Line line = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (line == null)
                throw new DO.BadLineIdException(line.LineId, "הקו אינו קיים במערכת");
            line.IsDeleted = true;
            //foreach (DO.LineTrip item in DataSource.ListLineTrips)
            //{
            //    if (item.LineId == lineId)
            //        item.IsDeleted = true;
            //}
            foreach (DO.LineStation item in DataSource.ListLineStations)
            {
                if (item.LineId == lineId)
                    item.IsDeleted = true;
            }
        }
        #endregion
        #region Trip
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from item in DataSource.ListTrips
                   select item.Clone();
        }
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
            return from item in DataSource.ListTrips
                   where predicate(item)
                   select item.Clone();
        }
        public DO.Trip GetTrip(int tripId)
        {
            DO.Trip trip = DataSource.ListTrips.Find(t => t.TripId == tripId);
            if (trip != null)
                return trip.Clone();
            else
                throw new DO.BadTripIdException(tripId, "ה------ אינו קיים במערכת");
        }
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(t => t.TripId == trip.TripId) != null)
                throw new DO.BadTripIdException(trip.TripId, "ה---- הינו קיים כבר במערכת");
            DataSource.ListTrips.Add(trip.Clone());
        }
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip newTrip = DataSource.ListTrips.Find(t => t.TripId == trip.TripId && t.IsDeleted == false);
            if (newTrip == null)
                throw new DO.BadTripIdException(trip.TripId, "ה------ אינו קיים במערכת");
            DO.Trip trip1 = trip.Clone(); //copy the trip to a new item
            newTrip.IsDeleted = true; // change the status of the trip to deleted
            DataSource.ListTrips.Add(trip1); // add the update new trip to the list
            //newTrip = trip1; //update the trip
        }
        public void UpdateTrip(int tripId, Action<DO.Trip> update)
        {

        }
        public void DeleteTrip(int tripId)
        {
            DO.Trip trip = DataSource.ListTrips.Find(t => t.TripId == tripId && t.IsDeleted == false);
            if (trip == null)
                throw new DO.BadTripIdException(tripId, "ה------ אינו קיים במערכת");
            trip.IsDeleted = true;
        }
        #endregion
        #region AdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from item in DataSource.ListAdjacentStations
                   where item.IsDeleted == false
                   select item.Clone();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            return from item in DataSource.ListAdjacentStations
                   where predicate(item)
                   select item.Clone();
        }
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == stationCode1 && a.StationCode2 == stationCode2 && a.IsDeleted == false || a.StationCode1 == stationCode2 && a.StationCode2 == stationCode1 && a.IsDeleted == false));
            if (adjacent != null)
                return adjacent.Clone();
            throw new DO.BadAdjacentStationsException(stationCode1, stationCode2, "התחנות העוקבות אינן קיימות במערכת");
        }
        public void AddAdjacentStations(DO.AdjacentStations adjacent)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(a => (a.StationCode1 == adjacent.StationCode1 && a.StationCode2 == adjacent.StationCode2 && a.IsDeleted == false || a.StationCode1 == adjacent.StationCode2 && a.StationCode2 == adjacent.StationCode1 && a.IsDeleted == false)) != null)
                throw new DO.BadAdjacentStationsException(adjacent.StationCode1, adjacent.StationCode2, "התחנות העוקבות שהקשת קיימות כבר במערכת");
            DataSource.ListAdjacentStations.Add(adjacent.Clone());
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjacent)
        {
            DO.AdjacentStations newAdjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == adjacent.StationCode1 && a.StationCode2 == adjacent.StationCode2 && a.IsDeleted == false || a.StationCode1 == adjacent.StationCode2 && a.StationCode2 == adjacent.StationCode1 && a.IsDeleted == false));
            if (newAdjacent != null)
                throw new DO.BadAdjacentStationsException(adjacent.StationCode1,adjacent.StationCode2, "התחנות העוקבות אינן קיימות במערכת");
            DO.AdjacentStations adjacent1 = adjacent.Clone(); //copy the adjacent stations to a new item
            newAdjacent.IsDeleted = true;
            DataSource.ListAdjacentStations.Add(adjacent1); 
        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            DO.AdjacentStations adjFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false));
            if (adjFind == null)
                throw new DO.BadAdjacentStationsException(stationCode1, stationCode2, "התחנות העוקבות אינן קיימות במערכת");
            update(adjFind);
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == stationCode1 && a.StationCode2 == stationCode2 && a.IsDeleted == false || a.StationCode1 == stationCode2 && a.StationCode2 == stationCode1 && a.IsDeleted == false));
            if (adjacent == null)
                throw new DO.BadAdjacentStationsException(stationCode1, stationCode2, "התחנות העוקבות אינן קיימות במערכת");
            adjacent.IsDeleted = true;
        }
        public bool IsExistAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == stationCode1 && a.StationCode2 == stationCode2 && a.IsDeleted == false));
            if (adjacent != null)
                return true;
            return false;
        }
        #endregion
        #region LineStation
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            return from item in DataSource.ListLineStations
                  // where item.IsDeleted == false
                   select item.Clone();
        }
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            return from item in DataSource.ListLineStations
                   where predicate(item)
                   select item.Clone();
        }
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStation = DataSource.ListLineStations.Find(l => (l.LineId == lineId && l.StationCode == stationCode && l.IsDeleted == false));
            if (lineStation != null)
                return lineStation.Clone();
            else
                throw new DO.BadLineStationException(lineId, stationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(l => (l.LineId == lineStation.LineId && l.StationCode == lineStation.StationCode && l.IsDeleted == false)) != null)
                throw new DO.BadLineStationException(lineStation.LineId, lineStation.StationCode, "תחנת הקו שהקשת קיימת כבר במערכת");          
            DataSource.ListLineStations.Add(lineStation.Clone());           
        }
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation newLineStation = DataSource.ListLineStations.Find(l => (l.LineId == lineStation.LineId && l.StationCode == lineStation.StationCode && l.IsDeleted == false));
            if (newLineStation == null)
                throw new DO.BadLineStationException(lineStation.LineId, lineStation.StationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
            DO.LineStation lineStation1 = lineStation.Clone(); //copy the line station to a new item
            DataSource.ListLineStations.Remove(newLineStation);
            DataSource.ListLineStations.Add(lineStation);
            //newLineStation = lineStation1; //update the line station
        }    
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            DO.LineStation lStatFind = DataSource.ListLineStations.Find(lStat => (lStat.LineId == lineId && lStat.StationCode == stationCode && lStat.IsDeleted == false));
            if (lStatFind == null)
                throw new DO.BadLineStationException(lineId, stationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
            update(lStatFind);
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            DO.LineStation newLineStation = DataSource.ListLineStations.Find(l => (l.LineId == lineId && l.StationCode == stationCode && l.IsDeleted == false));
            if (newLineStation == null)
                throw new DO.BadLineStationException(lineId, stationCode, "תחנת הקו שהקשת אינה קיימת במערכת");
            newLineStation.IsDeleted = true;
        }
        #endregion
        #region LineTrip
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            return from item in DataSource.ListLineTrips
                   select item.Clone();
        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            return from item in DataSource.ListLineTrips
                   where predicate(item)
                   select item.Clone();
        }
        public DO.LineTrip GetLineTrip(int lineId,TimeSpan time)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new Exception();
        }
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            if (DataSource.ListLineTrips.FirstOrDefault(l => l.LineId == lineTrip.LineId && l.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            DO.LineTrip newLineTrip = DataSource.ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.IsDeleted == false);
            if (newLineTrip == null)
                throw new Exception();
            DO.LineTrip lineTrip1 = lineTrip.Clone(); //copy the line trip to a new item
            newLineTrip = lineTrip1; //update the line trip
        }
        public void UpdateLineTrip(int lineId, TimeSpan time, Action<DO.LineTrip> update)
        {
            DO.LineTrip lTripFind = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.StartAt == time && l.IsDeleted == false);
            if (lTripFind == null)
                throw new DO.BadLineTripException(lineId, time, "נסיעת הקו אינה קיימת במערכת");
            update(lTripFind);
        }
        public void DeleteLineTrip(int lineId, TimeSpan time)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineTrip == null)
                throw new Exception();
            lineTrip.IsDeleted = true;
        }
        #endregion
        #region User
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from item in DataSource.ListUsers
                   select item.Clone();
        }
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            return from item in DataSource.ListUsers
                   where predicate(item)
                   select item.Clone();
        }
        public DO.User GetUser(string userName)
        {
            DO.User user = DataSource.ListUsers.Find(u => u.UserName == userName && u.IsDeleted == false);
            if (user != null)
                return user.Clone();
            else
                throw new DO.BadUserNameException(userName,"שם המשתמש שהקשת אינו קיים במערכת");
        }
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(u => u.UserName == user.UserName && u.IsDeleted == false) != null)
                throw new DO.BadUserNameException(user.UserName, "שם המשתמש שהקשת כבר קיים במערכת");
            DataSource.ListUsers.Add(user.Clone());
        }
        public void UpdateUser(DO.User user)
        {
            DO.User newUser = DataSource.ListUsers.Find(u => u.UserName == user.UserName && u.IsDeleted == false);
            if (newUser == null)
                throw new DO.BadUserNameException(user.UserName, "שם המשתמש שהקשת אינו קיים במערכת");
            DO.User user1 = user.Clone(); //copy the user to a new item
            newUser = user1; //update the user
        }
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            DO.User userFind = DataSource.ListUsers.Find(u => u.UserName == userName && u.IsDeleted == false);
            if (userFind == null)
                throw new DO.BadUserNameException(userName, "שם המשתמש שהקשת אינו קיים במערכת");
            update(userFind);
        }
        public void DeleteUser(string userName)
        {
            DO.User user = DataSource.ListUsers.Find(u => u.UserName == userName && u.IsDeleted == false);
            if (user == null)
                throw new DO.BadUserNameException(userName, "שם המשתמש שהקשת אינו קיים במערכת");
            user.IsDeleted = true;
        }
        #endregion
    }
}
