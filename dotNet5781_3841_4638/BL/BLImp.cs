using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
//using BO;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();

        #region Bus
            BO.Bus busDoBoAdapter(DO.Bus busDO)
            {
                BO.Bus busBO = new BO.Bus();
                busDO.CopyPropertiesTo(busBO);
                return busBO;
            }
            public void AddBus(BO.Bus bus)
            {
                DO.Bus busDO = new DO.Bus();
                bus.CopyPropertiesTo(busDO);
                try
                {
                    dl.AddBus(busDO);
                }
                catch (DO.BadLicenseNumException ex)
                {

                    throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
                }
                catch (DO.BadInputException ex)
                {

                    throw new BO.BadInputException(ex.Message);
                }
            }

            public void DeleteBus(int licenseNum)
            {
                try
                {
                    dl.DeleteBus(licenseNum);
                }
                catch (DO.BadLicenseNumException ex)
                {

                    throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
                }
            }

            public IEnumerable<BO.Bus> GetAllBuses()
            {
                return from item in dl.GetAllBuses()
                       select busDoBoAdapter(item);
            }

            public IEnumerable<BO.Bus> GetBusesBy(Predicate<BO.Bus> predicate)
            {
                throw new NotImplementedException();
            }

            public BO.Bus GetBus(int licenseNum)
            {
                DO.Bus busDO;
                try
                {
                    busDO = dl.GetBus(licenseNum);
                }
                catch (DO.BadLicenseNumException ex)
                {

                    throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
                }
                return busDoBoAdapter(busDO);
            }

            public void UpdateBusDetails(BO.Bus bus)
            {
                DO.Bus busDO = new DO.Bus();
                bus.CopyPropertiesTo(busDO);
                try
                {
                    dl.UpdateBus(busDO);
                }
                catch (DO.BadLicenseNumException ex)
                {
                    throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
                }
                catch (DO.BadInputException ex)
                {
                    throw new BO.BadInputException(ex.Message);
                }
            }
            #endregion
        #region Line
            BO.Line lineDoBoAdapter(DO.Line lineDO)
            {
                BO.Line lineBO = new BO.Line();
                int lineId = lineDO.LineId;                      
                lineDO.CopyPropertiesTo(lineBO);
                List<BO.StationInLine> stations = (from stat in dl.GetAllLineStationsBy(stat => stat.LineId == lineId && stat.IsDeleted == false) //Linestation
                                                   let station = dl.GetStation(stat.StationCode) //station
                                                   select station.CopyToStationInLine(stat)).ToList();
                stations = (stations.OrderBy(s => s.LineStationIndex)).ToList();
                foreach (BO.StationInLine item in stations)
                {
                    if (item.LineStationIndex != stations[stations.Count - 1].LineStationIndex)
                    {
                        int sc1 = item.StationCode;//station code 1
                        int sc2 = stations[item.LineStationIndex].StationCode;//station code 2
                        DO.AdjacentStations adjStat = dl.GetAdjacentStations(sc1, sc2);
                        item.Distance = adjStat.Distance;
                        item.Time = adjStat.Time;
                    }
                }           
                lineBO.stations = stations;
                return lineBO;
            }
            public IEnumerable<BO.Line> GetAllLines()
            {
                return from item in dl.GetAllLines()
                       select lineDoBoAdapter(item);
            }
            //public IEnumerable<BO.Line> GetAllLinesBy(Predicate<BO.Line> predicate)
            //{
            //    throw new NotImplementedException();
            //}
            public BO.Line GetLine(int lineId)
            {
                DO.Line lineDO;
                try
                {
                    lineDO = dl.GetLine(lineId);
                }
                catch (DO.BadLineIdException ex)
                {

                    throw new BO.BadLineIdException(ex.ID, ex.Message);
                }
                return lineDoBoAdapter(lineDO);
            }
            public void AddLine(BO.Line lineBo)
        {
            DO.Line lineDo = new DO.Line();//the DO.Line
            lineBo.CopyPropertiesTo(lineDo);
            lineDo.FirstStation = lineBo.stations[0].StationCode;//code of the first station
            lineDo.LastStation = lineBo.stations[lineBo.stations.Count - 1].StationCode;//code of the last station
            //בודק אם יש קו עם אותו מספר עם או תחנה סופית באותו איזור
            List<DO.Line> ltemp = dl.GetAllLinesBy(s => s.LineNum == lineDo.LineNum && s.LastStation == lineDo.LastStation && s.Area == lineDo.Area).ToList();
            if (ltemp.Count() != 0)
                throw new BO.BadInputException($"There is already a line with the number {lineDo.LineNum} in {lineDo.Area} with last station {lineDo.LastStation}");
            dl.AddLine(lineDo);//add line
            //עידכונים של תחנות עוקבות ותחנות קו
            lineDo.LineId = dl.GetAllLinesBy(s => s.LineNum == lineDo.LineNum && s.Area == lineDo.Area).FirstOrDefault().LineId;
            int sc1 = lineBo.stations[0].StationCode;//station Code of the first station
            int sc2 = lineBo.stations[1].StationCode;//station Code of the last station
            lineDo.FirstStation = sc1;
            lineDo.LastStation = sc2;
            try
            {
                if (!dl.IsExistAdjacentStations(sc1, sc2))//add to adjcenct stations if its not exists
                {
                    DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = sc1, StationCode2 = sc2, Distance = lineBo.stations[0].Distance, Time = lineBo.stations[0].Time };
                    dl.AddAdjacentStations(adj);
                }
                DO.LineStation first = new DO.LineStation() { LineId = lineDo.LineId, StationCode = sc1, LineStationIndex = lineBo.stations[0].LineStationIndex, IsDeleted = false, PrevStationCode = 0, NextStationCode = sc2 };
                DO.LineStation last = new DO.LineStation() { LineId = lineDo.LineId, StationCode = sc2, LineStationIndex = lineBo.stations[1].LineStationIndex, IsDeleted = false, PrevStationCode = sc1, NextStationCode = 0 };
                dl.AddLineStation(first);//add first line station
                dl.AddLineStation(last);//add last line ststion
            }
            catch (DO.BadTripIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);

                //DO.Line lineDO = new DO.Line();
                //line.CopyPropertiesTo(lineDO);
                ////lineDO.LineId = DO.Config.LineId++;
                //int sCode1 = line.stations[0].StationCode; //code of the first station
                //int sCode2 = line.stations[1].StationCode; //code of the second station
                //lineDO.FirstStation = sCode1;
                //lineDO.LastStation = sCode2;
                //try
                //{
                //    if (!dl.IsExistAdjacentStations(sCode1, sCode2))
                //    {
                //        DO.AdjacentStations adjacent = new DO.AdjacentStations { StationCode1 = sCode1, StationCode2 = sCode2, Distance = line.stations[0].Distance, Time = line.stations[0].Time, IsDeleted = false };
                //        dl.AddAdjacentStations(adjacent);
                //    }
                //    dl.AddLine(lineDO);
                //    DO.LineStation line1 = new DO.LineStation { LineId = lineDO.LineId, StationCode = sCode1, LineStationIndex = line.stations[0].LineStationIndex, IsDeleted = false };
                //    DO.LineStation line2 = new DO.LineStation { LineId = lineDO.LineId, StationCode = sCode2, LineStationIndex = line.stations[1].LineStationIndex, IsDeleted = false };
                //    dl.AddLineStation(line1);
                //    dl.AddLineStation(line2);
                //}
                //catch (DO.BadLineIdException ex)
                //{

                //    throw new BO.BadLineIdException(ex.ID, ex.Message);
                //}
            }
        }
            public void UpdateLineDetails(BO.Line line)
            {
                DO.Line lineDO = new DO.Line();
                line.CopyPropertiesTo(lineDO);
                try
                {
                    dl.UpdateLine(lineDO);
                }
                catch (DO.BadLineIdException ex)
                {

                    throw new BO.BadLineIdException(ex.ID, ex.Message);
                }

            }
            public void DeleteLine(int lineId)
            {
                try
                {
                    dl.DeleteLine(lineId);
                }
                catch (DO.BadLineIdException ex)
                {

                    throw new BO.BadLineIdException(ex.ID, ex.Message);
                }
            }
            #endregion
        #region LineStation
        public bool IsExistLineStation(DO.LineStation lineStation)
        {
            try
            {
                DO.LineStation newLineStation = dl.GetLineStation(lineStation.LineId, lineStation.StationCode);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void AddLineStation(BO.LineStation lineStation)
        {
            DO.LineStation LnStDO = (DO.LineStation)lineStation.CopyPropertiesToNew(typeof(DO.LineStation));
            try
            {
                if (IsExistLineStation(LnStDO))
                    throw new BO.BadLineStationException(LnStDO.LineId, LnStDO.StationCode, "התחנה הינה קיימת כבר במערכת");              
                List<DO.LineStation> list = (dl.GetAllLineStationsBy(sl => sl.LineId == LnStDO.LineId && sl.IsDeleted == false)).OrderBy(sl => sl.LineStationIndex).ToList();
                int last = list[list.Count - 1].LineStationIndex;
                if (LnStDO.LineStationIndex != last + 1) //if the line station is not the last one
                {
                    for (int i = LnStDO.LineStationIndex; i < last+1; i++) //Rebooting the list of line station
                    {
                        list[i - 1].LineStationIndex++;
                    }
                }
                DO.LineStation prevStation;
                DO.LineStation nextStation;
                if(LnStDO.LineStationIndex > 1) //if the station is not the first one
                {
                    prevStation = list[LnStDO.LineStationIndex - 2];
                    prevStation.NextStationCode = LnStDO.StationCode;
                    LnStDO.PrevStationCode = prevStation.StationCode;

                }
                if(LnStDO.LineStationIndex != last+1) //if the station is not the last one
                {
                    nextStation = list[LnStDO.LineStationIndex-1];
                    nextStation.PrevStationCode = LnStDO.StationCode;
                    LnStDO.NextStationCode = nextStation.StationCode;
                }
                foreach (DO.LineStation item in list)
                {
                    dl.UpdateLineStation(item);
                }
                dl.AddLineStation(LnStDO);
                List<DO.LineStation> list1= (dl.GetAllLineStationsBy(sl => sl.LineId == LnStDO.LineId && sl.IsDeleted == false)).OrderBy(sl => sl.LineStationIndex).ToList();               
                if (LnStDO.LineStationIndex != list[list.Count - 1].LineStationIndex) //if the station is not the last station
                {
                    nextStation = list1[LnStDO.LineStationIndex];
                    if (!IsExistAdjacentStations(LnStDO.StationCode, nextStation.StationCode))
                    {
                        DO.AdjacentStations adjacentStations = new DO.AdjacentStations() { StationCode1 = LnStDO.StationCode, StationCode2 = nextStation.StationCode };
                        dl.AddAdjacentStations(adjacentStations);
                    }
                }
                if (LnStDO.LineStationIndex != 1) //if the station is not the first station
                {
                    prevStation = list1[LnStDO.LineStationIndex - 2];
                    if (!IsExistAdjacentStations(LnStDO.StationCode, prevStation.StationCode))
                    {
                        DO.AdjacentStations adjacentStations = new DO.AdjacentStations() { StationCode1 = prevStation.StationCode, StationCode2 = LnStDO.StationCode };
                        dl.AddAdjacentStations(adjacentStations);
                    }
                }
            }
            catch (BO.BadLineStationException ex)
            {

                throw new BO.BadLineStationException(ex.lineId, ex.stationCode, ex.Message);
            }
            catch (BO.BadAdjacentStationsException ex)
            {

                throw new BO.BadAdjacentStationsException(ex.stationCode1,ex.stationCode2,ex.Message);
            }
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            try
            {
                DO.LineStation deleteStation = dl.GetLineStation(lineId, stationCode);
                BO.Line line = GetLine(lineId);
                if (line.stations.Count <= 2)
                    throw new BO.BadInputException("קיימות בקו 2 תחנות או פחות, אין אפשרות למחוק את התחנה המבוקשת");
                //------
                if(line.stations[0].StationCode != stationCode && line.stations[line.stations.Count-1].StationCode != stationCode) //if the selected station is not the firs or the last one
                {
                    BO.StationInLine prevStation = line.stations[deleteStation.LineStationIndex - 2];
                    BO.StationInLine nextStation = line.stations[deleteStation.LineStationIndex];
                    if (!dl.IsExistAdjacentStations(prevStation.StationCode, nextStation.StationCode))
                    {
                        DO.AdjacentStations adjacentStations = new DO.AdjacentStations() { StationCode1 = prevStation.StationCode, StationCode2 = nextStation.StationCode, IsDeleted = false };
                        dl.AddAdjacentStations(adjacentStations);
                    }
                }
                //---------
                List<DO.LineStation> list = (dl.GetAllLineStationsBy(sl => sl.LineId == deleteStation.LineId && sl.IsDeleted == false)).OrderBy(sl => sl.LineStationIndex).ToList();
                DO.LineStation next;
                if(deleteStation.LineStationIndex > 1) //the selcted station is not the first one
                {
                    DO.LineStation prev = list[deleteStation.LineStationIndex - 2];
                    if (deleteStation.LineStationIndex != list[list.Count - 1].LineStationIndex) //the selected station is not the last one
                    {
                        next = list[deleteStation.LineStationIndex];
                        prev.NextStationCode = next.StationCode;
                        next.PrevStationCode = prev.StationCode;
                    }
                    else //the selected station is the last one
                        prev.NextStationCode = 0;
                }
                else //the selcted station is the first one
                {
                    if(deleteStation.LineStationIndex != list[list.Count - 1].LineStationIndex) //the selected station is not the last one
                    {
                        next = list[deleteStation.LineStationIndex];
                        next.PrevStationCode = 0;
                    }
                }
                //-------
                if (deleteStation.LineStationIndex != list[list.Count - 1].LineStationIndex) //if the station is not the last one
                {
                    for (int i = deleteStation.LineStationIndex; i < list.Count; i++) //Rebooting the list of line station
                    {
                        list[i - 1].LineStationIndex++;
                    }
                }
                foreach (DO.LineStation item in list)
                {
                    dl.UpdateLineStation(item);
                }
                dl.DeleteLineStation(lineId, stationCode);
            }
            catch (DO.BadLineStationException ex)
            {
                throw new BO.BadLineStationException(ex.lineId, ex.stationCode, ex.Message);
            }
            catch (BO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
            catch (DO.BadAdjacentStationsException ex)
            {
                throw new BO.BadAdjacentStationsException(ex.stationCode1, ex.stationCode2);
            }
        }
        #endregion
        #region AdjacentStation
        public bool IsExistAdjacentStations(int sc1, int sc2)
        {
            if (dl.IsExistAdjacentStations(sc1, sc2))
                return true;
            return false;
        }
        #endregion
        #region Station
        public BO.Station StationDoBoAdapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            int stationCode = stationDO.Code;
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.Lines = (from item in dl.GetAllLineStationsBy(stat => stat.StationCode == stationCode)
                               let line = dl.GetLine(item.LineId)
                               select line.CopyToLineInStation(item)).ToList();
            return stationBO;

        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select StationDoBoAdapter(item);
        }
        public void AddStation(BO.Station station)
        {
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            stationDO.IsDeleted = false;
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
        }
        public void DeleteStation(int stationCode)
        {
            try
            {
                DO.Station stationDO = dl.GetStation(stationCode);
                BO.Station stationBO = StationDoBoAdapter(stationDO);
                if (stationBO.Lines.Count != 0)
                    throw new BO.BadStationCodeException(stationCode, "אין אפשרות למחוק את התחנה הנוכחית מכיון שיש קווים נוספים שעוברים בה");
                dl.DeleteStation(stationCode);
            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

        }
        public void UpdateStation(BO.Station stationBO)
        {
            try
            {
                DO.Station stationDO = new DO.Station();
                stationBO.CopyPropertiesTo(stationDO);
                stationDO.IsDeleted = false;
                dl.UpdateStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
        }
        #endregion
        #region StationInLine
        public void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second)
        {
            try
            {
                DO.AdjacentStations adjacent = new DO.AdjacentStations { StationCode1 = first.StationCode, StationCode2 = second.StationCode, Distance = first.Distance, Time = first.Time, IsDeleted = false };
                dl.UpdateAdjacentStations(adjacent);
            }
            catch (DO.BadAdjacentStationsException ex)
            {

                throw new BO.BadAdjacentStationsException(ex.stationCode1, ex.stationCode2, ex.Message);
            }
            //catch
            //{
            //    throw new 
            //}
        }
        #endregion
        #region User
        public void AddUser(BO.User userBO)
        {
            try
            {
                DO.User userDO = new DO.User();
                userBO.CopyPropertiesTo(userDO);
                userDO.IsDeleted = false;
                dl.AddUser(userDO);
            }
            catch (DO.BadUserNameException ex)
            {

                throw new BO.BadUserNameException(ex.userName, ex.Message);
            }
        }
        public BO.User SignIn(string userName, string password)
        {
            BO.User userBO = new BO.User();
            try
            {               
                DO.User userDO = dl.GetUser(userName);
                if (password != userDO.Password)
                    throw new BO.BadUserNameException(userName, "שם המשתמש או הסיסמא שהקשת שגויים");
                userDO.CopyPropertiesTo(userBO);               
            }
            catch (DO.BadUserNameException ex)
            {
                throw new BO.BadUserNameException(ex.userName, ex.Message);
            }
            return userBO;
        }
        #endregion
    }
}
