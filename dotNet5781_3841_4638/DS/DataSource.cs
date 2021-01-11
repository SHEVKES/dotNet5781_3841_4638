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
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //2
                {
                    UserName= "ayala6521",
                    Password= "abc33",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //3
                {
                    UserName= "tahel87",
                    Password= "df456",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //4
                {
                    UserName= "dav983",
                    Password= "pro865",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //5
                {
                    UserName= "duc4569",
                    Password= "xzxz",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //6
                {
                    UserName= "cut765",
                    Password= "fuyfuy",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //7
                {
                    UserName= "dog555",
                    Password= "digdig",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //8
                {
                    UserName= "fug897",
                    Password= "strstr",
                    AdminAccess=false,
                    IsDeleted= false
                },

                new User //9
                {
                    UserName= "noa8642",
                    Password= "ttt456",
                    AdminAccess=true,
                    IsDeleted= false
                },

                new User //10
                {
                    UserName= "classb",
                    Password= "shalom4",
                    AdminAccess=false,
                    IsDeleted= false
                },
            };
            #endregion

            #region ListLineStation
            ListLineStations = new List<LineStation>
            {
                //line Id=0
                
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
                //line Id=1
                //new LineStation
                //{
                //    LineId=1,
                //    StationCode=84,
                //    LineStationIndex=0,
                //    PrevStationCode=76,
                //    NextStationCode=0,
                //    IsDeleted=false,
                //}
            };
            #endregion

            #region ListAdjacentStations
            ListAdjacentStations = new List<AdjacentStations>()
            {
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
                    StationCode1=76,
                    StationCode2 =119,
                    Distance=6.5,
                    Time=new TimeSpan(0,2,0),
                    IsDeleted=false,
                },
            };
            #endregion
        }
    }
}
