using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BO;

namespace BLAPI
{
    public interface IBL
    {
        //Add Person to Course
        //get all courses for student
        //etc...
        #region Bus
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetBusesBy(Predicate<BO.Bus> predicate);
        BO.Bus GetBus(int licenseNum);
        void AddBus(BO.Bus bus);
        void UpdateBusDetails(BO.Bus bus);
        void DeleteBus(int licenseNum);
        #endregion
        #region Line
        IEnumerable<BO.Line> GetAllLines();
        //IEnumerable<BO.Line> GetAllLinesBy(Predicate<BO.Line> predicate);
        BO.Line GetLine(int lineId);
        void AddLine(BO.Line line);
        void UpdateLineDetails(BO.Line line);
        void DeleteLine(int lineId);
        #endregion
        #region LineStation
        void AddLineStation(BO.LineStation lineStation);
        void DeleteLineStation(int lineId, int stationCode);
        #endregion
        #region AdjacentStation
        bool IsExistAdjacentStations(int sc1, int sc2);
        #endregion
        #region Station
        IEnumerable<BO.Station> GetAllStations();
        void AddStation(BO.Station station);
        void DeleteStation(int stationCode);
        void UpdateStation(BO.Station stationBO);
        #endregion
        #region StationInLine
        void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second);
        #endregion
        #region User
        void AddUser(BO.User userBO);
        BO.User SignIn(string userName, string password);
        #endregion
    }
}
