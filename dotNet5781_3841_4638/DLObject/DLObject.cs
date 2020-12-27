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
            throw new Exception();
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            throw new Exception();
        }
        public DO.Bus GetBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum);
            if (bus != null)
              return bus.Clone();
            else
                throw new Exception();
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
                throw new Exception();
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum);
            if (busFind == null)
                throw new Exception();
            DO.Bus bus1 = bus.Clone(); //copy the bus to a new item
            busFind = bus1; //update the bus
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)
        {

        }
        public void DeleteBus(int licenseNum)
        {

        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            throw new Exception();
        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            throw new Exception();
        }
        public DO.Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(s => s.Code == code);
            if (station != null)
                return station.Clone();
            else
                throw new Exception();
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(s => s.Code == station.Code) != null)
                throw new Exception();
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station st = DataSource.ListStations.Find(s => s.Code == station.Code);
            if (st == null)
                throw new Exception();
            DO.Station station1 = station.Clone(); //copy the station to a new item
            st = station1; //update the station
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {

        }
        public void DeleteStation(int code)
        {

        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            throw new Exception();
        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            throw new Exception();
        }
        public DO.Line GetLine(int lineId)
        {
            DO.Line line = DataSource.ListLines.Find(l => l.LineId == lineId);
            if (line != null)
                return line.Clone();
            else
                throw new Exception();
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(l => l.LineId == line.LineId) != null)
                throw new Exception();
            DataSource.ListLines.Add(line.Clone());           
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line newLine = DataSource.ListLines.Find(l => l.LineId == line.LineId);
            if (newLine == null)
                throw new Exception();
            DO.Line line1 = line.Clone(); //copy the line to a new item
            newLine = line1; //update the line
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {

        }
        public void DeleteLine(int lineId)
        {

        }
        #endregion
        #region Trip
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            throw new Exception();
        }
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
            throw new Exception();
        }
        public DO.Trip GetTrip(int tripId)
        {
            DO.Trip trip = DataSource.ListTrips.Find(t => t.TripId == tripId);
            if (trip != null)
                return trip.Clone();
            else
                throw new Exception();
        }
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(t => t.TripId == trip.TripId) != null)
                throw new Exception();
            DataSource.ListTrips.Add(trip.Clone());
        }
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip newTrip = DataSource.ListTrips.Find(t => t.TripId == trip.TripId);
            if (newTrip == null)
                throw new Exception();
            DO.Trip trip1 = trip.Clone(); //copy the trip to a new item
            newTrip = trip1; //update the trip
        }
        public void UpdateTrip(int tripId, Action<DO.Trip> update)
        {

        }
        public void DeleteTrip(int tripId)
        {

        }
        #endregion
        #region AdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            throw new Exception();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            throw new Exception();
        }
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == stationCode1 && a.StationCode2 == stationCode2));
            if (adjacent != null)
                return adjacent.Clone();
            else
                throw new Exception();
        }
        public void AddAdjacentStations(DO.AdjacentStations adjacent)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(a => (a.StationCode1 == adjacent.StationCode1 && a.StationCode2 == adjacent.StationCode2))!= null)
                throw new Exception();
            DataSource.ListAdjacentStations.Add(adjacent.Clone());
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjacent)
        {
            DO.AdjacentStations newAdjacent = DataSource.ListAdjacentStations.Find(a => (a.StationCode1 == adjacent.StationCode1 && a.StationCode2 == adjacent.StationCode2));
            if (newAdjacent != null)
                throw new Exception();
            DO.AdjacentStations adjacent1 = adjacent.Clone(); //copy the adjacent stations to a new item
            newAdjacent = adjacent1; //update the adjacent stations
        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {

        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {

        }
        #endregion
    }
}
