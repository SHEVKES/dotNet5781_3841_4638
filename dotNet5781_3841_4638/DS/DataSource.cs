using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DS
{
    public static class DataSource
    {
        public static List<Bus> ListBuses;
        public static List<Station> ListStations;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrips;
        public static List<User> ListUsers;
        public static List<AdjacentStations> ListAdjacentStations;
        public static List<Trip> ListTrips;
        public static List<Line> ListLines;
        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            #region ListStations 
            ListStations = new List<Station>
            {
                new Station
                {
                    Code = 73,
                    Name = "שדרות גולדה מאיר/המשורר אצ''ג",
                    Address = "רחוב:שדרות גולדה מאיר  עיר: ירושלים ",
                    Latitude = 31.825302,
                    Longitude = 35.188624,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 76,
                    Name = "בית ספר צור באהר בנות/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים",
                    Latitude = 31.738425,
                    Longitude = 35.228765,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 77,
                    Name = "בית ספר אבן רשד/אלמדינה אלמונוורה",
                    Address = "רחוב:אל מדינה אל מונאוורה  עיר: ירושלים ",
                    Latitude = 31.738676,
                    Longitude = 35.226704,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 78,
                    Name = "שרי ישראל/יפו",
                    Address = "רחוב:שדרות שרי ישראל 15 עיר: ירושלים",
                    Latitude = 31.789128,
                    Longitude = 35.206146,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 83,
                    Name = "בטן אלהווא/חוש אל מרג",
                    Address = "רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.766358,
                    Longitude = 35.240417,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 84,
                    Name = "מלכי ישראל/הטורים",
                    Address = " רחוב:מלכי ישראל 77 עיר: ירושלים ",
                    Latitude = 31.790758,
                    Longitude = 35.209791,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 85,
                    Name = "בית ספר לבנים/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.768643,
                    Longitude = 35.238509,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 86,
                    Name = "מגרש כדורגל/אלמדארס",
                    Address = "רחוב:אלמדארס  עיר: ירושלים",
                    Latitude = 31.769899,
                    Longitude = 35.23973,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 88,
                    Name = "בית ספר לבנות/בטן אלהוא",
                    Address = " רחוב:בטן אל הווא  עיר: ירושלים",
                    Latitude = 31.767064,
                    Longitude = 35.238443,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 89,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765863,
                    Longitude = 35.247198,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 90,
                    Name = "גולדה/הרטום",
                    Address = "רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.799804,
                    Longitude = 35.213021,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 91,
                    Name = "דרך בית לחם הישה/ואדי קדום",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים ",
                    Latitude = 31.765717,
                    Longitude = 35.247102,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 93,
                    Name = "חוש סלימה 1",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767265,
                    Longitude = 35.246594,
                    DisabledAccess = false,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 94,
                    Name = "דרך בית לחם הישנה ב",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.767084,
                    Longitude = 35.246655,
                    DisabledAccess = false,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 95,
                    Name = "דרך בית לחם הישנה א",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.768759,
                    Longitude = 31.768759,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 97,
                    Name = "שכונת בזבז 2",
                    Address = " רחוב:דרך בית לחם הישנה  עיר: ירושלים",
                    Latitude = 31.77002,
                    Longitude = 35.24348,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 102,
                    Name = "גולדה/שלמה הלוי",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8003,
                    Longitude = 35.208257,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 103,
                    Name = "גולדה/הרטום",
                    Address = " רחוב:שדרות גולדה מאיר  עיר: ירושלים",
                    Latitude = 31.8,
                    Longitude = 35.214106,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 105,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 2 עיר: ירושלים",
                    Latitude = 31.797708,
                    Longitude = 35.217133,
                    DisabledAccess = false,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 106,
                    Name = "גבעת משה",
                    Address = " רחוב:גבעת משה 3 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longitude = 35.217057,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                //20
                new Station
                {
                    Code = 108,
                    Name = "עזרת תורה/עלי הכהן",
                    Address = "  רחוב:עזרת תורה 25 עיר: ירושלים",
                    Latitude = 31.797535,
                    Longitude = 35.213728,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 109,
                    Name = "עזרת תורה/דורש טוב",
                    Address = "  רחוב:עזרת תורה 21 עיר: ירושלים ",
                    Latitude = 31.796818,
                    Longitude = 35.212936,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 110,
                    Name = "עזרת תורה/דורש טוב",
                    Address = " רחוב:עזרת תורה 12 עיר: ירושלים",
                    Latitude = 31.796129,
                    Longitude = 35.212698,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 111,
                    Name = "יעקובזון/עזרת תורה",
                    Address = "  רחוב:יעקובזון 1 עיר: ירושלים",
                    Latitude = 31.794631,
                    Longitude = 35.21161,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 112,
                    Name = "יעקובזון/עזרת תורה",
                    Address = " רחוב:יעקובזון  עיר: ירושלים",
                    Latitude = 31.79508,
                    Longitude = 35.211684,
                    DisabledAccess = false,
                    IsDeleted = false
                },
                //25
                new Station
                {
                    Code = 113,
                    Name = "זית רענן/אוהל יהושע",
                    Address = "  רחוב:זית רענן 1 עיר: ירושלים",
                    Latitude = 31.796255,
                    Longitude = 35.211065,
                    DisabledAccess = false,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 115,
                    Name = "זית רענן/תורת חסד",
                    Address = " רחוב:זית רענן  עיר: ירושלים",
                    Latitude = 31.798423,
                    Longitude = 35.209575,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 116,
                    Name = "זית רענן/תורת חסד",
                    Address = "  רחוב:הרב סורוצקין 48 עיר: ירושלים ",
                    Latitude = 31.798689,
                    Longitude = 35.208878,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 117,
                    Name = "קרית הילד/סורוצקין",
                    Address = "  רחוב:הרב סורוצקין  עיר: ירושלים",
                    Latitude = 31.799165,
                    Longitude = 35.206918
                },
                new Station
                {
                    Code = 119,
                    Name = "סורוצקין/שנירר",
                    Address = "  רחוב:הרב סורוצקין 31 עיר: ירושלים",
                    Latitude = 31.797829,
                    Longitude = 35.205601,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                //30
                new Station
                {
                    Code = 1485,
                    Name = "שדרות נווה יעקוב/הרב פרדס ",
                    Address = "רחוב: שדרות נווה יעקוב  עיר:ירושלים ",
                    Latitude = 31.840063,
                    Longitude = 35.240062,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1486,
                    Name = "מרכז קהילתי /שדרות נווה יעקוב",
                    Address = "רחוב:שדרות נווה יעקוב ירושלים עיר:ירושלים ",
                    Latitude = 31.838481,
                    Longitude = 35.23972,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1487,
                    Name = " מסוף 700 /שדרות נווה יעקוב ",
                    Address = "חוב:שדרות נווה יעקב 7 עיר: ירושלים  ",
                    Latitude = 31.837748,
                    Longitude = 35.231598,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1488,
                    Name = " הרב פרדס/אסטורהב ",
                    Address = "רחוב:מעגלות הרב פרדס  עיר: ירושלים רציף  ",
                    Latitude = 31.840279,
                    Longitude = 35.246272,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1490,
                    Name = "הרב פרדס/צוקרמן ",
                    Address = "רחוב:מעגלות הרב פרדס 24 עיר: ירושלים   ",
                    Latitude = 31.843598,
                    Longitude = 35.243639,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1491,
                    Name = "ברזיל ",
                    Address = "רחוב:ברזיל 14 עיר: ירושלים",
                    Latitude = 31.766256,
                    Longitude = 35.173,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1492,
                    Name = "בית וגן/הרב שאג ",
                    Address = "רחוב:בית וגן 61 עיר: ירושלים ",
                    Latitude = 31.76736,
                    Longitude = 35.184771,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1493,
                    Name = "בית וגן/עוזיאל ",
                    Address = "רחוב:בית וגן 21 עיר: ירושלים    ",
                    Latitude = 31.770543,
                    Longitude = 35.183999,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1494,
                    Name = " קרית יובל/שמריהו לוין ",
                    Address = "רחוב:ארתור הנטקה  עיר: ירושלים    ",
                    Latitude = 31.768465,
                    Longitude = 35.178701,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1510,
                    Name = " קורצ'אק / רינגלבלום ",
                    Address = "רחוב:יאנוש קורצ'אק 7 עיר: ירושלים",
                    Latitude = 31.759534,
                    Longitude = 35.173688,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1511,
                    Name = " טהון/גולומב ",
                    Address = "רחוב:יעקב טהון  עיר: ירושלים     ",
                    Latitude = 31.761447,
                    Longitude = 35.175929,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1512,
                    Name = "הרב הרצוג/שח''ל ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.761447,
                    Longitude = 35.199936,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1514,
                    Name = "פרץ ברנשטיין/נזר דוד ",
                    Address = "רחוב:הרב הרצוג  עיר: ירושלים רציף",
                    Latitude = 31.759186,
                    Longitude = 35.189336,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1518,
                    Name = "פרץ ברנשטיין/נזר דוד",
                    Address = " רחוב:פרץ ברנשטיין 56 עיר: ירושלים ",
                    Latitude = 31.759121,
                    Longitude = 35.189178,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1522,
                    Name = "מוזיאון ישראל/רופין",
                    Address = "  רחוב:דרך רופין  עיר: ירושלים ",
                    Latitude = 31.774484,
                    Longitude = 35.204882,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1523,
                    Name = "הרצוג/טשרניחובסקי",
                    Address = "   רחוב:הרב הרצוג  עיר: ירושלים  ",
                    Latitude = 31.769652,
                    Longitude = 35.208248,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 1524,
                    Name = "רופין/שד' הזז",
                    Address = "    רחוב:הרב הרצוג  עיר: ירושלים   ",
                    Latitude = 31.769652,
                    Longitude = 35.208248,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 121,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = " רחוב:הרב סורוצקין 13 עיר: ירושלים",
                    Latitude = 31.796033,
                    Longitude =35.206094,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 123,
                    Name = "אוהל דוד/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 9 עיר: ירושלים",
                    Latitude = 31.794958,
                    Longitude =35.205216,
                    DisabledAccess = true,
                    IsDeleted = false
                },
                new Station
                {
                    Code = 122,
                    Name = "מרכז סולם/סורוצקין ",
                    Address = "  רחוב:הרב סורוצקין 28 עיר: ירושלים",
                    Latitude = 31.79617,
                    Longitude =35.206158,
                    DisabledAccess = true,
                    IsDeleted = false
                }
            };
            #endregion

            #region ListBuses
            ListBuses = new List<Bus>
            {
                new Bus//1
                {
                    LicenseNum= 12345678,
                    FromDate= new DateTime(2018, 12, 1),
                    TotalTrip=10000,
                    FuelRemain=1200,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 12,1 ),
                    KmLastTreat=8001,
                    IsDeleted=false
                },
                new Bus//2
                {
                    LicenseNum= 1524897,
                    FromDate= new DateTime(2017, 12, 1),
                    TotalTrip=10000,
                    FuelRemain=900,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 12,1 ),
                    KmLastTreat=9500,
                    IsDeleted=false
                },
                new Bus//3
                {
                    LicenseNum= 45698725,
                    FromDate= new DateTime(2019, 12, 11),
                    TotalTrip=10000,
                    FuelRemain=1000,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 12,1 ),
                    KmLastTreat=9700,
                    IsDeleted=false
                },
                 new Bus//4
                {
                    LicenseNum= 47589646,
                    FromDate= new DateTime(2019, 11, 11),
                    TotalTrip=10000,
                    FuelRemain=800,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 9,2),
                    KmLastTreat=9600,
                    IsDeleted=false
                },
                 new Bus//5
                {
                    LicenseNum= 1456982,
                    FromDate= new DateTime(2016, 11, 2),
                    TotalTrip=10000,
                    FuelRemain=800,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 12,2),
                    KmLastTreat=9600,
                    IsDeleted=false
                },
                  new Bus//6
                {
                    LicenseNum= 1458795,
                    FromDate= new DateTime(2015, 11,3),
                    TotalTrip=20000,
                    FuelRemain=1200,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 8,21),
                    KmLastTreat=19600,
                    IsDeleted=false
                },
                    new Bus//7
                {
                    LicenseNum= 65984758,
                    FromDate= new DateTime(2019, 8,2),
                    TotalTrip=30000,
                    FuelRemain=800,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020, 5,15),
                    KmLastTreat=20000,
                    IsDeleted=false
                },
                  new Bus//8
                {
                    LicenseNum= 4569821,
                    FromDate= new DateTime(2014, 11,20),
                    TotalTrip=10000,
                    FuelRemain=800,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020,6,10),
                    KmLastTreat=9600,
                    IsDeleted=false
                },
                   new Bus//9
                {
                    LicenseNum= 2564875,
                    FromDate= new DateTime(2013, 11,2),
                    TotalTrip=50000,
                    FuelRemain=800,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020,10,1),
                    KmLastTreat=49700,
                    IsDeleted=false
                },
                   new Bus//10
                {
                    LicenseNum= 42650314,
                    FromDate= new DateTime(2019, 1,20),
                    TotalTrip=10000,
                    FuelRemain=1200,
                    Status=BusStatus.Available,
                    DateLastTreat=new DateTime(2020,7,1),
                    KmLastTreat=9900,
                    IsDeleted=false
                },
            };
            #endregion

            #region ListLines
            ListLines = new List<Line>
            {
                new Line //1
                {
                    LineId= Config.LineId++,
                    LineNum=33,
                    Area= Area.Jerusalem,
                    FirstStation=91, //גולדה/הרטום
                    LastStation=119, //קרית הילד/סורוצקין
                    IsDeleted=false
                },
                new Line //2
                {
                    LineId=Config.LineId++,
                    LineNum=12,
                    Area= Area.Jerusalem,
                    FirstStation=84, //מלכי ישראל/הטורים
                    LastStation=1492, //בית וגן/הרב שאג
                    IsDeleted=false
                },
                   new Line //3
                {
                    LineId=Config.LineId++,
                    LineNum=53,
                    Area= Area.Jerusalem,
                    FirstStation=78, //שרי ישראל/יפו
                    LastStation=1511, //טהון/גולומב
                    IsDeleted=false
                },
                new Line //4
                {
                    LineId=Config.LineId++,
                    LineNum=240,
                    Area= Area.Jerusalem,
                    FirstStation=102,//גולדה/שלמה הלוי
                    LastStation=122, //מרכז סולם/סורוצקין
                    IsDeleted=false
                },
                new Line //5
                {
                    LineId=Config.LineId++,
                    LineNum=74,
                    Area= Area.Jerusalem,
                    FirstStation=105, //גבעת משה
                    LastStation=1490, //הרב פרדס/צוקרמן
                    IsDeleted=false
                },
                new Line //6
                {
                    LineId=Config.LineId++,
                    LineNum=9,
                    Area= Area.Jerusalem,
                    FirstStation=123, //אוהל דוד/סורוצקין
                    LastStation=1491, //ברזיל
                    IsDeleted=false
                },
                new Line //7
                {
                    LineId=Config.LineId++,
                    LineNum=139,
                    Area= Area.Jerusalem,
                    FirstStation=1518, //פרץ ברנשטיין/נזר דוד
                    LastStation=116, //זית רענן/תורת חסד
                    IsDeleted=false
                },
                 new Line //8
                {
                    LineId=Config.LineId++,
                    LineNum=68,
                    Area= Area.Jerusalem,
                    FirstStation=108, //עזרת תורה/עלי הכהן
                    LastStation=97, //שכונת בזבז 2
                    IsDeleted=false
                },
                  new Line //9
                {
                    LineId=Config.LineId++,
                    LineNum=82,
                    Area= Area.Jerusalem,
                    FirstStation=111, //יעקובזון/עזרת תורה
                    LastStation=1493, //בית וגן/עוזיאל
                    IsDeleted=false
                },
                   new Line //10
                {
                    LineId=Config.LineId++,
                    LineNum=67,
                    Area= Area.Jerusalem,
                    FirstStation=1512, //הרב הרצוג/ שח"ל
                    LastStation=113, //זית רענן/אוהל יהושע
                    IsDeleted=false
                },
            };
            #endregion

            #region ListUsers
            ListUsers = new List<User>
            {
                new User //1
                {
                    UserName= "shira123",
                    Password="sh123",
                    FirstName="Shira",
                    LastName="Shevkes",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //2
                {
                    UserName= "ayala6521",
                    Password= "abc33",
                    FirstName="Ayala",
                    LastName="Cohen",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //3
                {
                    UserName= "tahel87",
                    Password= "df456",
                    FirstName="Tahel",
                    LastName="Sharon",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //4
                {
                    UserName= "dav983",
                    Password= "pro865",
                    FirstName="David",
                    LastName="Guetta",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //5
                {
                    UserName= "duc4569",
                    Password= "xzxz",
                    FirstName="Eli",
                    LastName="Shechter",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //6
                {
                    UserName= "cut765",
                    Password= "fuyfuy",
                    FirstName="Michael",
                    LastName="Moyal",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //7
                {
                    UserName= "dog555",
                    Password= "digdig",
                    FirstName="Ariel",
                    LastName="Etinger",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //8
                {
                    UserName= "fug897",
                    Password= "strstr",
                    FirstName="Romi",
                    LastName="Rayen",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //9
                {
                    UserName= "noa8642",
                    Password= "ttt456",
                    FirstName="Noa",
                    LastName="Timsit",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //10
                {
                    UserName= "classb",
                    Password= "shalom4",
                    FirstName="Asaf",
                    LastName="Hertz",
                    AdminAccess=false,
                    IsDeleted= false
                },
            };
            #endregion

            #region ListLineStation
            ListLineStations = new List<LineStation>
            {
                #region Line #1

                new LineStation
                {
                    LineId=0,
                    StationCode=73,
                    LineStationIndex=2,
                    PrevStationCode=91,
                    NextStationCode=76,
                    IsDeleted=false,

                },
                 new LineStation
                {
                    LineId=0,
                    StationCode=91,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=73,
                    IsDeleted=false,

                },
                new LineStation
                {
                    LineId=0,
                    StationCode=76,
                    LineStationIndex=3,
                    PrevStationCode=73,
                    NextStationCode=119,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=0,
                    StationCode=119,
                    LineStationIndex=4,
                    PrevStationCode=76,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #2
                new LineStation
                {
                    LineId=1,
                    StationCode=84,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=83,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=83,
                    LineStationIndex=2,
                    PrevStationCode=84,
                    NextStationCode=78,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=78,
                    LineStationIndex=3,
                    PrevStationCode=83,
                    NextStationCode=77,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=77,
                    LineStationIndex=4,
                    PrevStationCode=78,
                    NextStationCode=1492,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=1,
                    StationCode=1492,
                    LineStationIndex=5,
                    PrevStationCode=77,
                    NextStationCode=0,
                    IsDeleted=false,
                },

                #endregion
                #region Line #3
                new LineStation
                {
                    LineId=2,
                    StationCode=90,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=88,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=88,
                    LineStationIndex=2,
                    PrevStationCode=90,
                    NextStationCode=86,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=86,
                    LineStationIndex=3,
                    PrevStationCode=88,
                    NextStationCode=85,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=85,
                    LineStationIndex=4,
                    PrevStationCode=86,
                    NextStationCode=1511,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=2,
                    StationCode=1511,
                    LineStationIndex=5,
                    PrevStationCode=85,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #4
                new LineStation
                {
                    LineId=3,
                    StationCode=102,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=97,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=97,
                    LineStationIndex=2,
                    PrevStationCode=102,
                    NextStationCode=95,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=95,
                    LineStationIndex=3,
                    PrevStationCode=97,
                    NextStationCode=94,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=94,
                    LineStationIndex=4,
                    PrevStationCode=95,
                    NextStationCode=122,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=3,
                    StationCode=122,
                    LineStationIndex=5,
                    PrevStationCode=94,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #5
                new LineStation
                {
                    LineId=4,
                    StationCode=105,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=109,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=109,
                    LineStationIndex=2,
                    PrevStationCode=105,
                    NextStationCode=108,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=108,
                    LineStationIndex=3,
                    PrevStationCode=109,
                    NextStationCode=106,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=106,
                    LineStationIndex=4,
                    PrevStationCode=108,
                    NextStationCode=1490,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=4,
                    StationCode=1490,
                    LineStationIndex=5,
                    PrevStationCode=106,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #6
                new LineStation
                {
                    LineId=5,
                    StationCode=123,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=121,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=121,
                    LineStationIndex=2,
                    PrevStationCode=123,
                    NextStationCode=1524,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1524,
                    LineStationIndex=3,
                    PrevStationCode=121,
                    NextStationCode=1523,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1523,
                    LineStationIndex=4,
                    PrevStationCode=1524,
                    NextStationCode=1491,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=5,
                    StationCode=1491,
                    LineStationIndex=5,
                    PrevStationCode=1523,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #7
                new LineStation
                {
                    LineId=6,
                    StationCode=1518,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=1522,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=1522,
                    LineStationIndex=2,
                    PrevStationCode=1518,
                    NextStationCode=1514,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=1514,
                    LineStationIndex=3,
                    PrevStationCode=1522,
                    NextStationCode=93,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=93,
                    LineStationIndex=4,
                    PrevStationCode=1514,
                    NextStationCode=116,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=6,
                    StationCode=116,
                    LineStationIndex=5,
                    PrevStationCode=93,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #8
                new LineStation
                {
                    LineId=7,
                    StationCode=103,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=113,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=113,
                    LineStationIndex=2,
                    PrevStationCode=103,
                    NextStationCode=112,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=112,
                    LineStationIndex=3,
                    PrevStationCode=113,
                    NextStationCode=111,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=111,
                    LineStationIndex=4,
                    PrevStationCode=112,
                    NextStationCode=115,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=7,
                    StationCode=115,
                    LineStationIndex=5,
                    PrevStationCode=111,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #9
                new LineStation
                {
                    LineId=8,
                    StationCode=110,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=1486,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=1486,
                    LineStationIndex=2,
                    PrevStationCode=110,
                    NextStationCode=1485,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=1485,
                    LineStationIndex=3,
                    PrevStationCode=1486,
                    NextStationCode=117,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=117,
                    LineStationIndex=4,
                    PrevStationCode=1485,
                    NextStationCode=1493,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=8,
                    StationCode=1493,
                    LineStationIndex=5,
                    PrevStationCode=117,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
                #region Line #10
                new LineStation
                {
                    LineId=9,
                    StationCode=1512,
                    LineStationIndex=1,
                    PrevStationCode=0,
                    NextStationCode=1488,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=1488,
                    LineStationIndex=2,
                    PrevStationCode=1512,
                    NextStationCode=1487,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=1487,
                    LineStationIndex=3,
                    PrevStationCode=1488,
                    NextStationCode=1510,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=1510,
                    LineStationIndex=4,
                    PrevStationCode=1487,
                    NextStationCode=1494,
                    IsDeleted=false,
                },
                new LineStation
                {
                    LineId=9,
                    StationCode=1494,
                    LineStationIndex=5,
                    PrevStationCode=1510,
                    NextStationCode=0,
                    IsDeleted=false,
                },
                #endregion
            };
            #endregion

            #region ListAdjacentStations
            ListAdjacentStations = new List<AdjacentStations>()
            {
                #region Line #1
                new AdjacentStations
                {
                    StationCode1=91,
                    StationCode2 = 73,
                    Distance=4.5,
                    Time=new TimeSpan(0,5,0),
                    IsDeleted=false,
                },
                new AdjacentStations
                {
                    StationCode1=73,
                    StationCode2 = 76,
                    Distance=3.5,
                    Time=new TimeSpan(0,3,0),
                    IsDeleted=false,
                },
                new AdjacentStations
                {
                    StationCode1 = 76,
                    StationCode2 = 119,
                    Distance = 6.5,
                    Time = new TimeSpan(0, 2, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #2
                new AdjacentStations
                {
                    StationCode1 = 84,
                    StationCode2 = 83,
                    Distance = 5,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 83,
                    StationCode2 = 78,
                    Distance = 7,
                    Time = new TimeSpan(0, 5, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 78,
                    StationCode2 = 77,
                    Distance = 2,
                    Time = new TimeSpan(0, 3, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 77,
                    StationCode2 = 1492,
                    Distance = 6,
                    Time = new TimeSpan(0, 8, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #3
                new AdjacentStations
                {
                    StationCode1 = 90,
                    StationCode2 = 88,
                    Distance = 3,
                    Time = new TimeSpan(0, 4, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 88,
                    StationCode2 = 86,
                    Distance = 5,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 86,
                    StationCode2 = 85,
                    Distance = 8,
                    Time = new TimeSpan(0, 12, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 85,
                    StationCode2 = 1511,
                    Distance = 10,
                    Time = new TimeSpan(0, 15, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #4
                new AdjacentStations
                {
                    StationCode1 = 102,
                    StationCode2 = 97,
                    Distance = 8,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 97,
                    StationCode2 = 95,
                    Distance = 9,
                    Time = new TimeSpan(0, 11, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 95,
                    StationCode2 = 94,
                    Distance = 10,
                    Time = new TimeSpan(0, 13, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 94,
                    StationCode2 = 122,
                    Distance = 5,
                    Time = new TimeSpan(0, 7, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #5
                new AdjacentStations
                {
                    StationCode1 = 105,
                    StationCode2 = 109,
                    Distance = 4.5,
                    Time = new TimeSpan(0, 5, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 109,
                    StationCode2 = 108,
                    Distance = 3,
                    Time = new TimeSpan(0, 5, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 108,
                    StationCode2 = 106,
                    Distance = 7.8,
                    Time = new TimeSpan(0, 10, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 106,
                    StationCode2 = 1490,
                    Distance = 5.6,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #6
                new AdjacentStations
                {
                    StationCode1 = 123,
                    StationCode2 = 121,
                    Distance = 6,
                    Time = new TimeSpan(0, 8, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 121,
                    StationCode2 = 1524,
                    Distance = 9,
                    Time = new TimeSpan(0, 11, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1524,
                    StationCode2 = 1523,
                    Distance = 6.8,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1523,
                    StationCode2 = 1491,
                    Distance = 4.6,
                    Time = new TimeSpan(0, 8, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #7
                new AdjacentStations
                {
                    StationCode1 = 1518,
                    StationCode2 = 1522,
                    Distance = 6,
                    Time = new TimeSpan(0, 5, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1522,
                    StationCode2 = 1514,
                    Distance = 9,
                    Time = new TimeSpan(0, 12, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1514,
                    StationCode2 = 93,
                    Distance = 4,
                    Time = new TimeSpan(0, 4, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 93,
                    StationCode2 = 116,
                    Distance = 6,
                    Time = new TimeSpan(0, 10, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #8
                new AdjacentStations
                {
                    StationCode1 = 103,
                    StationCode2 = 113,
                    Distance = 8,
                    Time = new TimeSpan(0, 10, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 113,
                    StationCode2 = 112,
                    Distance = 6,
                    Time = new TimeSpan(0, 8, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 112,
                    StationCode2 = 111,
                    Distance = 3,
                    Time = new TimeSpan(0, 4, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 111,
                    StationCode2 = 115,
                    Distance = 6,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #9
                new AdjacentStations
                {
                    StationCode1 = 110,
                    StationCode2 = 1486,
                    Distance = 3.5,
                    Time = new TimeSpan(0, 3, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1486,
                    StationCode2 = 1485,
                    Distance = 2,
                    Time = new TimeSpan(0, 2, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1485,
                    StationCode2 = 117,
                    Distance = 5,
                    Time = new TimeSpan(0, 6, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 117,
                    StationCode2 = 1493,
                    Distance = 5.7,
                    Time = new TimeSpan(0, 8, 0),
                    IsDeleted = false,
                },
                #endregion
                #region Line #10
                new AdjacentStations
                {
                    StationCode1 = 1512,
                    StationCode2 = 1488,
                    Distance = 6,
                    Time = new TimeSpan(0, 9, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1488,
                    StationCode2 = 1487,
                    Distance = 9.5,
                    Time = new TimeSpan(0, 10, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1487,
                    StationCode2 = 1510,
                    Distance = 8,
                    Time = new TimeSpan(0, 11, 0),
                    IsDeleted = false,
                },
                new AdjacentStations
                {
                    StationCode1 = 1510,
                    StationCode2 = 1494,
                    Distance = 3,
                    Time = new TimeSpan(0, 5, 0),
                    IsDeleted = false,
                },
                #endregion
            };
            #endregion

            #region ListLineTrip
            ListLineTrips = new List<LineTrip>()
            {
                //line Id 0
                new LineTrip
                {
                    LineId=0,
                    StartAt=new TimeSpan(5,0,0),
                },
                new LineTrip
                {
                    LineId=0,
                    StartAt=new TimeSpan(5,20,0),
                },
                new LineTrip
                {
                    LineId=0,
                    StartAt=new TimeSpan(5,10,0),
                },
                //line Id 1
                new LineTrip
                {
                    LineId=1,
                    StartAt=new TimeSpan(5,8,0),
                },
                //line Id 2
                new LineTrip
                {
                    LineId=2,
                    StartAt=new TimeSpan(9,8,0),
                },
                 //line Id 3
                new LineTrip
                {
                    LineId=3,
                    StartAt=new TimeSpan(14,8,0),
                },
                 //line Id 4
                new LineTrip
                {
                    LineId=4,
                    StartAt=new TimeSpan(18,8,0),
                },
                 //line Id 5
                new LineTrip
                {
                    LineId=5,
                    StartAt=new TimeSpan(10,3,0),
                },
                 //line Id 6
                new LineTrip
                {
                    LineId=6,
                    StartAt=new TimeSpan(8,3,0),
                },
                 //line Id 7
                new LineTrip
                {
                    LineId=7,
                    StartAt=new TimeSpan(16,2,0),
                },
                 //line Id 8
                new LineTrip
                {
                    LineId=8,
                    StartAt=new TimeSpan(13,2,0),
                },
                 //line Id 9
                new LineTrip
                {
                    LineId=9,
                    StartAt=new TimeSpan(14,45,0),
                },
            };
            #endregion

        }
    }
}
