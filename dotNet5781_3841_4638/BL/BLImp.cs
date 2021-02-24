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
        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion
        #region Bus
        BO.Bus busDoBoAdapter(DO.Bus busDO) //turns a DO bus to a BO bus object
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        private int LengthLicenseNum(int licenseNum) //returns the length of the license number it gets
        {
            int counter = 0;
            while (licenseNum != 0)
            {
                licenseNum = licenseNum / 10;
                counter++;
            }
            return counter;
        }
        public void AddBus(BO.Bus bus) //add new bus
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try  //check the input
            {
                int length = LengthLicenseNum(busDO.LicenseNum);
                if ((length == 7 && bus.FromDate.Year >= 2018) || ((length == 8 && bus.FromDate.Year < 2018)))
                    //!(length == 7 && bus.FromDate.Year < 2018) || (length == 8 && bus.FromDate.Year >= 2018))
                    throw new BO.BadInputException("מספר הרשוי שהקשת אינו תקין");
                if (busDO.FromDate > DateTime.Now)
                    throw new BO.BadInputException("תאריך התחלת הפעילות שהקשת אינו תקין");
                if (busDO.TotalTrip < 0)
                    throw new BO.BadInputException("סך הקילומטרים שהקשת אינו תקין");
                if (busDO.FuelRemain < 0 || busDO.FuelRemain > 1200)
                    throw new BO.BadInputException("כמות הדלק שהקשת אינה תקינה");
                if (busDO.DateLastTreat < busDO.FromDate || busDO.DateLastTreat > DateTime.Now)
                    throw new BO.BadInputException("התאריך של הטיפול האחרון שהקשת אינו תקין");
                if (busDO.KmLastTreat < 0 || busDO.KmLastTreat > busDO.TotalTrip)
                    throw new BO.BadInputException("סך הקילומטרים מאז הטיפול האחרון שהקשת אינו תקין");
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
        public void DeleteBus(int licenseNum) //The function deletes a bus from the list
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
        //public IEnumerable<BO.Bus> GetBusesBy(Predicate<BO.Bus> predicate)
        //{
        //    throw new NotImplementedException();
        //}
        public BO.Bus GetBus(int licenseNum) //The function returns a single bus according to its license number
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
        public void UpdateBusDetails(BO.Bus bus) //Updates details of a particular bus
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
        public void RefuelBus(BO.Bus busBO)//refuel the bus
        {
            try
            {
                DO.Bus busDO = dl.GetBus(busBO.LicenseNum);
                if (busDO.FuelRemain == 1200)
                    throw new BO.BadInputException("מיכל הדלק של האוטובוס כבר מלא");
                busDO.FuelRemain = 1200;
                busDO.Status = DO.BusStatus.Available;
                dl.UpdateBus(busDO);

            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum,ex.Message);
            }
        }
        public void TreatmentBus(BO.Bus busBO)//treat the bus
        {
            try
            {
                DO.Bus busDO = dl.GetBus(busBO.LicenseNum);
                if (busDO.DateLastTreat.ToShortDateString() == DateTime.Now.ToShortDateString())//if the bus is already treated
                    throw new BO.BadInputException("האוטובוס כבר עבר טיפול");
                busDO.DateLastTreat = DateTime.Now;
                busDO.KmLastTreat = busDO.TotalTrip;
                dl.UpdateBus(busDO);

            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum,ex.Message);
            }
        }

        #endregion
        #region Line
        BO.Line lineDoBoAdapter(DO.Line lineDO) //adapter of DO.Line=> BO.line
        {
            BO.Line lineBO = new BO.Line(); //the BO.line that will return
            int lineId = lineDO.LineId; //the line id of the DO.Line
            lineDO.CopyPropertiesTo(lineBO);
            List<BO.StationInLine> stations = (from stat in dl.GetAllLineStationsBy(stat => stat.LineId == lineId && stat.IsDeleted == false) //Linestation
                                               let station = dl.GetStation(stat.StationCode) //station
                                               select station.CopyToStationInLine(stat)).ToList(); 
            stations = (stations.OrderBy(s => s.LineStationIndex)).ToList(); //the stations of the line order by their index
            foreach (BO.StationInLine item in stations) //the distance and time between each adjacent stations
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
            lineBO.stations = stations; //enter the list of stations with the time and the distance to the BO.Line
            lineBO.DepTimes = (from lineTrip in dl.GetAllLineTripsBy(lineTrip => lineTrip.LineId == lineId && lineTrip.IsDeleted == false) //line trip
                               select lineTrip.StartAt).OrderBy(s => s.TotalMinutes).ToList();
            return lineBO;
        }
        public IEnumerable<BO.Line> GetAllLines() //returns a collection of all lines
        {
            return from item in dl.GetAllLines()
                   select lineDoBoAdapter(item);
        }
        //public IEnumerable<BO.Line> GetAllLinesBy(Predicate<BO.Line> predicate)
        //{
        //    throw new NotImplementedException();
        //}
        public BO.Line GetLine(int lineId) //returns a particular line according to its line ID
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
        public void AddLine(BO.Line lineBo) //Adds a new line
        {
            DO.Line lineDo = new DO.Line();//the DO.Line
            lineBo.CopyPropertiesTo(lineDo);
            lineDo.FirstStation = lineBo.stations[0].StationCode;//code of the first station
            lineDo.LastStation = lineBo.stations[lineBo.stations.Count - 1].StationCode;//code of the last station
            // Checks if there is a line with the same number with or a terminus in the same area
            List<DO.Line> ltemp = dl.GetAllLinesBy(s => s.LineNum == lineDo.LineNum && s.LastStation == lineDo.LastStation && s.Area == lineDo.Area).ToList();
            if (ltemp.Count() != 0)
                throw new BO.BadInputException($"קיים כבר קו עם המספר {lineDo.LineNum} ב {lineDo.Area} התחנה האחרונה היא {lineDo.LastStation}");
            dl.AddLine(lineDo);//add line
            //Updates to consecutive stations and line stations
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
            }
        }
        public void UpdateLineDetails(BO.Line line) //Updates the details of the line
        {
            DO.Line lineDO = new DO.Line(); 
            line.CopyPropertiesTo(lineDO);
            lineDO.FirstStation = line.stations[0].StationCode; //the code of the first station in the line
            lineDO.LastStation = line.stations[line.stations.Count - 1].StationCode; //the code of the last station in the line
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.BadLineIdException ex)
            {

                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
        } 
        public void DeleteLine(int lineId) //Deletes the line and also at every station where the line passes deletes the line
        {
            try
            {
                dl.DeleteLine(lineId);
                //delete from the line station list
                List<DO.LineStation> listLineStations = dl.GetAllLineStationsBy(s => s.LineId == lineId).ToList();
                foreach (DO.LineStation s in listLineStations)
                {
                    dl.DeleteLineStation(s.LineId, s.StationCode);
                }
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
        public BO.Station StationDoBoAdapter(DO.Station stationDO) //turns a DO station to a BO station object
        {
            BO.Station stationBO = new BO.Station(); //station BO
            int stationCode = stationDO.Code; //station code
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.Lines = (from item in dl.GetAllLineStationsBy(stat => stat.StationCode == stationCode && stat.IsDeleted==false) //line station
                               let line = dl.GetLine(item.LineId) //line
                               select line.CopyToLineInStation(item)).ToList();
            foreach (BO.LineInStation item in stationBO.Lines)//restart the last station for each line 
            {
                var line = dl.GetLine(item.LineId);
                var station = dl.GetStation(line.LastStation);
                item.NameLastStation = station.Name;
            }
            return stationBO;

        }
        public IEnumerable<BO.Station> GetAllStations() //returns a collection of all stations
        {
            return from item in dl.GetAllStations()
                   select StationDoBoAdapter(item);
        }
        public void AddStation(BO.Station station) //add new station
        {
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            stationDO.IsDeleted = false;
            if (stationDO.Longitude < 31 || stationDO.Longitude > 33.3)//check input
            {
                throw new BO.BadInputException("הערך של קו האורך שגוי, עליך להכניס ערך בין 31 עד 33.3");
            }
            if (stationDO.Latitude < 34.3 || stationDO.Latitude > 35.5)//check input
            {
                throw new BO.BadInputException("הערך של קו הרוחב שגוי, עליך להכניס ערך בין 34.3 עד 35.5");
            }
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {

                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
        }
        public void DeleteStation(int stationCode) //Deletes the station from the list of stations as well as from the list of following stations
        {
            try
            {
                DO.Station stationDO = dl.GetStation(stationCode);
                BO.Station stationBO = StationDoBoAdapter(stationDO);
                if (stationBO.Lines.Count != 0) //if there are lines that stop in the station
                    throw new BO.BadStationCodeException(stationCode, "אין אפשרות למחוק את התחנה הנוכחית מכיון שיש קווים נוספים שעוברים בה");
                dl.DeleteStation(stationCode);
                List<DO.AdjacentStations> listAdj = dl.GetAllAdjacentStations().ToList();
                foreach (DO.AdjacentStations s in listAdj)//delete from adjacent Station list
                {
                    if (s.StationCode1 == stationCode || s.StationCode2 == stationCode)
                        dl.DeleteAdjacentStations(s.StationCode1, s.StationCode2);
                }
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
        }
        public void UpdateStation(BO.Station stationBO) //Updates station information
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
        #region Simulator
        public IEnumerable<BO.LineTiming> GetLineTimingPerStation(BO.Station stationBO, TimeSpan currentTime)
        {
            //list of lines that pass in the station
            List<BO.Line> listLines = (from l in GetAllLines()
                                       where l.stations.Find(s => s.StationCode == stationBO.Code) != null
                                       select l).ToList();

            List<BO.LineTiming> times = new List<BO.LineTiming>();//list of the lines that the function will return
            TimeSpan hour = new TimeSpan(1, 0, 0);//help to find the times that in the range of one hour from currentTime                           
            for (int i = 0; i < listLines.Count(); i++)//for all the lines that pass in the station
            {//calculate the times 
                TimeSpan tmp;//the current time
                int currentLineid = listLines[i].LineId;// line id of the current line
                List<DO.LineTrip> lineSchedual = dl.GetAllLineTripsBy(trip => trip.LineId == currentLineid && trip.IsDeleted == false).ToList();// times of the current Line
                TimeSpan timeTilStatin = travelTime(stationBO.Code, currentLineid);
                int numOfTimes = 0;//רוצים רק 3 זמנים לקו כמו שזה במציאות
                List<int> timesOfCurrentLine = new List<int>();//רשימת הזמנים
                for (int j = 0; j < lineSchedual.Count && numOfTimes < 3; j++)//for all the times in line sSchedual
                {
                    //check if currentTime-LeavingTime-travelTime more than zero and in the range of hour
                    if (lineSchedual[j].StartAt + timeTilStatin <= currentTime + hour
                        && lineSchedual[j].StartAt + timeTilStatin >= currentTime)
                    //check if the bus already passed the statioin   
                    {
                        if (currentTime - lineSchedual[j].StartAt >= TimeSpan.Zero)
                        //if the line already get out from the station
                        {
                            tmp = timeTilStatin - (currentTime - lineSchedual[j].StartAt);
                        }
                        else//if the line didnt get out from the station
                            tmp = timeTilStatin + (lineSchedual[j].StartAt - currentTime);

                        timesOfCurrentLine.Add(tmp.Minutes);
                        //timesString = timesString + tmp.Minutes+ ", ";                                                                                                  
                        numOfTimes++;
                    }
                }

                if (timesOfCurrentLine.Count != 0)//אם יש זמנים לקו בטווח של שעה
                {
                    string timesString = "";//the string of times
                    timesOfCurrentLine = timesOfCurrentLine.OrderBy(s => s).ToList();//order the times in ascending order
                    for (int k = 0; k < timesOfCurrentLine.Count - 1; k++)
                    {
                        timesString = timesString + timesOfCurrentLine[k] + ", ";
                    }
                    timesString = timesString + timesOfCurrentLine[timesOfCurrentLine.Count - 1];//add the last one without ","
                    times.Add(new BO.LineTiming//הוספת הקו לרשימה שהולכים להחזיר
                    {
                        LineId = currentLineid,
                        LineNum = listLines[i].LineNum,
                        DestinationStation = listLines[i].stations[listLines[i].stations.Count() - 1].Name,
                        Stringtimes = timesString,
                    });

                }
                numOfTimes = 0;//איפוס המונה
            }
            times = times.OrderBy(lt => lt.LineNum).ToList();//order the list by the number of the lines in ascending order
            return times;

        }
        private TimeSpan travelTime(int stationCode, int lineID)
        {//func that return the time from first station in line to specific station
            TimeSpan sumTime = TimeSpan.Zero;
            BO.Line line = GetLine(lineID);
            foreach (var s in line.stations)
            {
                if (s.StationCode != stationCode)
                    sumTime += s.Time;
                else
                {
                    break;
                }
            }
            return sumTime;
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
