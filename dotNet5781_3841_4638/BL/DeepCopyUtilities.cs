﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class DeepCopyUtilities
    {
        public static void CopyPropertiesTo<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = typeof(S).GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CopyPropertiesToNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type); // new object of Type
            from.CopyPropertiesTo(to);
            return to;
        }
        //public static BO.StudentCourse CopyToStudentCourse(this DO.Course course, DO.StudentInCourse sic)
        //{
        //    BO.StudentCourse result = (BO.StudentCourse)course.CopyPropertiesToNew(typeof(BO.StudentCourse));
        //    // propertys' names changed? copy them here...
        //    result.Grade = sic.Grade;
        //    return result;
        //}       
        public static BO.StationInLine CopyToStationInLine(this DO.Station station, DO.LineStation lineStation)
        {
            BO.StationInLine stationInLine = new BO.StationInLine();
            station.CopyPropertiesTo(stationInLine);
            stationInLine.LineStationIndex = lineStation.LineStationIndex;
            stationInLine.StationCode = lineStation.StationCode;
            return stationInLine;
        }
        public static BO.LineInStation CopyToLineInStation(this DO.Line l, DO.LineStation s)
        {
            BO.LineInStation lineInStation = new BO.LineInStation();
            l.CopyPropertiesTo(lineInStation);
            lineInStation.LineStationIndex = s.LineStationIndex;
            return lineInStation;
        }
    }
}

