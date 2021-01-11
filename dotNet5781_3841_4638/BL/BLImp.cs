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
            public void AddLine(BO.Line line)
            {
                DO.Line lineDO = new DO.Line();
                line.CopyPropertiesTo(lineDO);
                lineDO.LineId = DO.Config.LineId++;
                int sCode1 = line.stations[0].StationCode; //code of the first station
                int sCode2 = line.stations[1].StationCode; //code of the second station
                lineDO.FirstStation = sCode1;
                lineDO.LastStation = sCode2;
                try
                {
                    if (!dl.IsExistAdjacentStations(sCode1, sCode2))
                    {
                        DO.AdjacentStations adjacent = new DO.AdjacentStations { StationCode1 = sCode1, StationCode2 = sCode2, Distance = line.stations[0].Distance, Time = line.stations[0].Time, IsDeleted = false };
                        dl.AddAdjacentStations(adjacent);
                    }
                    dl.AddLine(lineDO);
                    DO.LineStation line1 = new DO.LineStation { LineId = lineDO.LineId, StationCode = sCode1, LineStationIndex = line.stations[0].LineStationIndex, IsDeleted = false };
                    DO.LineStation line2 = new DO.LineStation { LineId = lineDO.LineId, StationCode = sCode2, LineStationIndex = line.stations[1].LineStationIndex, IsDeleted = false };
                    dl.AddLineStation(line1);
                    dl.AddLineStation(line2);
                }
                catch (DO.BadLineIdException ex)
                {

                    throw new BO.BadLineIdException(ex.ID, ex.Message);
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
           public void AddLineStation(BO.LineStation lineStation)
           {
                DO.LineStation LnStDO = (DO.LineStation)lineStation.CopyPropertiesToNew(typeof(DO.LineStation));
                try
                {
                    dl.AddLineStation(LnStDO);
                    List<DO.LineStation> list = (dl.GetAllLineStationsBy(sl => sl.LineId == LnStDO.LineId && sl.IsDeleted == false)).OrderBy(sl => sl.LineStationIndex).ToList();
                    if (lineStation.LineStationIndex != list[list.Count-1].LineStationIndex) //if the station is not the last station
                    {
                        DO.LineStation next = list[lineStation.LineStationIndex];
                        if(!IsExistAdjacentStations(lineStation.StationCode,next.StationCode))
                        {
                            DO.AdjacentStations adjacentStations = new DO.AdjacentStations() { StationCode1=lineStation.StationCode,StationCode2=next.StationCode};
                            dl.AddAdjacentStations(adjacentStations);
                        }
                    }
                    if(lineStation.LineStationIndex != 1) //if the station is not the first station
                    {
                        DO.LineStation prev = list[lineStation.LineStationIndex - 2];
                        if (!IsExistAdjacentStations(lineStation.StationCode, prev.StationCode))
                        {
                            DO.AdjacentStations adjacentStations = new DO.AdjacentStations() { StationCode1 = prev.StationCode, StationCode2 = lineStation.StationCode };
                            dl.AddAdjacentStations(adjacentStations);
                        }
                    }              
                }
                catch (Exception)
                {

                    throw;
                }
           }
           public void DeleteLineStation(int lineId, int stationCode)
           {
                try
                {
                    dl.DeleteLineStation(lineId, stationCode);
                }
                catch (Exception)
                {

                    throw;
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
    }
}
