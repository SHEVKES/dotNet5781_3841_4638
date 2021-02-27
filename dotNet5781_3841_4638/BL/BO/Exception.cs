using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    [Serializable]
    public class BadLicenseNumException : Exception
    {
        public int licenseNum;
        public BadLicenseNumException(int ln) : base() => licenseNum = ln;
        public BadLicenseNumException(int ln, string message) :
            base(message) => licenseNum = ln;
        public BadLicenseNumException(int ln, string message, Exception innerException) :
            base(message, innerException) => licenseNum = ln;

        public override string ToString() => base.ToString() + $", bad license number: {licenseNum}";
    }
    public class BadInputException : Exception
    {
        public BadInputException(string message) : base(message) { }
        public override string ToString() => base.ToString();
    }
    public class BadLineIdException : Exception
    {
        public int ID;
        public BadLineIdException(int id) : base() => ID = id;
        public BadLineIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $", bad Line ID number: {ID}";
    }
    public class BadStationCodeException : Exception
    {
        public int stationCode;
        public BadStationCodeException(int code) : base() => stationCode = code;
        public BadStationCodeException(int code, string message) :
            base(message) => stationCode = code;
        public BadStationCodeException(int code, string message, Exception innerException) :
            base(message, innerException) => stationCode = code;

        public override string ToString() => base.ToString() + $", bad station code number: {stationCode}";
    }
    public class BadAdjacentStationsException : Exception
    {
        public int stationCode1;
        public int stationCode2;
        public BadAdjacentStationsException(int _stationCode1, int _stationCode2) : base() { stationCode1 = _stationCode1; stationCode2 = _stationCode2; }
        public BadAdjacentStationsException(int _stationCode1, int _stationCode2, string message) :
            base(message)
        { stationCode1 = _stationCode1; stationCode2 = _stationCode2; }
        public BadAdjacentStationsException(int _stationCode1, int _stationCode2, string message, Exception innerException) :
            base(message, innerException)
        { stationCode1 = _stationCode1; stationCode2 = _stationCode2; }
        public override string ToString()
        {
            return Message;
        }
    }
    public class BadLineStationException : Exception
    {
        public int lineId;
        public int stationCode;
        public BadLineStationException(int _lineId, int _stationCode) : base() { lineId = _lineId; stationCode = _stationCode; }
        public BadLineStationException(int _lineId, int _stationCode, string message) :
            base(message)
        { lineId = _lineId; stationCode = _stationCode; }
        public BadLineStationException(int _lineId, int _stationCode, string message, Exception innerException) :
            base(message, innerException)
        { lineId = _lineId; stationCode = _stationCode; }

        //public override string ToString() => base.ToString() + $", bad station code number: {userName}";
        public override string ToString()
        {
            return Message;
        }
    }
    public class BadUserNameException : Exception
    {
        public string userName;
        public BadUserNameException(string name) : base() => userName = name;
        public BadUserNameException(string name, string message) :
            base(message) => userName = name;
        public BadUserNameException(string name, string message, Exception innerException) :
            base(message, innerException) => userName = name;
        public override string ToString() => base.ToString() + $", bad user name: {userName}";
    }
    public class BadLineTripException : Exception
    {
        public int lineId;
        public TimeSpan depTime;//departure time
        public BadLineTripException(string message, Exception innerException) :
         base(message, innerException)
        {
            lineId = ((DO.BadLineTripException)innerException).lineId;
            depTime = ((DO.BadLineTripException)innerException).depTime;
        }
        public override string ToString()
        {
            return Message;
        }
    }
}
