using AsrtoUtils.Conversion;
using AstroUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsrtoUtils
{
    public static class AstroUtilsProp
    {
        // OBSERVATORY LOCATION
        //Moscow
        //static public double Latitude = 55.9452777777778;
        //static public double Longitude = 38.7133333333333;
        //Vedrus
        static public double Latitude = 44.7;
        static public double Longitude = 38.6;

        static public double SiteTimeZone = 3;

        /// <summary>
        /// Atuo constructor
        /// </summary>
        static AstroUtilsProp()
        {
        }


        static public DateTime SunSetDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.SunSetDateTimeUtc());
        }
        static public DateTime SunRiseDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.SunRiseDateTimeUtc());
        }


        static public DateTime CivilTwilightSetDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.CivilTwilightSetDateTimeUtc());
        }
        static public DateTime CivilTwilightRiseDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.CivilTwilightRiseDateTimeUtc());
        }


        static public DateTime NautTwilightSetDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.NautTwilightSetDateTimeUtc());
        }
        static public DateTime NautTwilightRiseDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.NautTwilightRiseDateTimeUtc());
        }
        

        static public DateTime AstronTwilightSetDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.AstronTwilightSetDateTimeUtc());
        }
        static public DateTime AstronTwilightRiseDateTime(int DayShift = 0)
        {
            return DateTimeUtils.ConvertToLocal(AstroUtilsProp.AstronTwilightRiseDateTimeUtc());
        }






        static public DateTime SunSetDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;

            Sunriset.SunriseSunset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);
            return DateTimeUtils.ConvertToDateTime(Math.Abs(tsunset), DayShift, DateTimeKind.Utc);
        }
        static public DateTime SunRiseDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;

            Sunriset.SunriseSunset(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);
            return DateTimeUtils.ConvertToDateTime(Math.Abs(tsunrise), DayShift, DateTimeKind.Utc);
        }



        static public DateTime CivilTwilightSetDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;

            Sunriset.CivilTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);
            //sunsetTime = TimeSpan.FromHours(tsunset);
            //sunsetTimeString = sunsetTime.ToString(@"hh\:mm\:ss");

            return DateTimeUtils.ConvertToDateTime(tsunset, DayShift, DateTimeKind.Utc);
        }
        static public DateTime CivilTwilightRiseDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;

            Sunriset.CivilTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);

            return DateTimeUtils.ConvertToDateTime(tsunrise, DayShift, DateTimeKind.Utc);
        }


        static public DateTime NautTwilightSetDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;
            Sunriset.NauticalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);

            return DateTimeUtils.ConvertToDateTime(tsunset, DayShift, DateTimeKind.Utc);
        }
        static public DateTime NautTwilightRiseDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;
            Sunriset.NauticalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);

            return DateTimeUtils.ConvertToDateTime(tsunrise, DayShift, DateTimeKind.Utc);
        }




        static public DateTime AstronTwilightSetDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;
            Sunriset.AstronomicalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);

            return DateTimeUtils.ConvertToDateTime(tsunset, DayShift, DateTimeKind.Utc);
        }
        static public DateTime AstronTwilightRiseDateTimeUtc(int DayShift = 0)
        {
            double tsunrise, tsunset;
            Sunriset.AstronomicalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, Latitude, Longitude, out tsunrise, out tsunset);

            return DateTimeUtils.ConvertToDateTime(tsunrise, DayShift, DateTimeKind.Utc);
        }

        /************************************************************************************************************
         * 
         * Moon Calcs
         * 
         ************************************************************************************************************/

        public static void getMoonTimesForDate(out DateTime outMoonRise, out DateTime outMoonSet)
        {
            MoonClass.calculateMoonTimes(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Latitude, Longitude, SiteTimeZone, out outMoonRise, out outMoonSet);
        }

        public static void getMoonTimesForDate(DateTime CurDate, double lat, double lon, double timezoneoffset, out DateTime outMoonRise, out DateTime outMoonSet)
        {
            DateTime CurSession = CurDate.AddHours(-12);
            //int Date = ;

            DateTime MoonRiseDT0, MoonSetDT0;
            DateTime MoonRiseDT1, MoonSetDT1;
            DateTime MoonRiseDT_1, MoonSetDT_1;
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.Day, lat, lon, timezoneoffset, out MoonRiseDT0, out MoonSetDT0);
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.AddDays(1).Day, lat, lon, timezoneoffset, out MoonRiseDT1, out MoonSetDT1);
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.AddDays(-1).Day, lat, lon, timezoneoffset, out MoonRiseDT_1, out MoonSetDT_1);

            DateTime MoonRise = DateTime.MinValue;
            DateTime MoonSet = DateTime.MinValue;

            if (MoonSetDT0.Hour < MoonRiseDT0.Hour && (MoonSetDT0.Day == CurSession.Day) && (MoonRiseDT0.Day == CurSession.Day))
            {
                //Console.Write(" ..." + MoonSetDT0.ToString("MM-dd H:m"));
                //Console.Write("  " + MoonRiseDT0.ToString("MM-dd H:m") + "...");
                MoonSet = MoonSetDT0;
                MoonRise = MoonRiseDT0;
            }
            else if ((MoonSetDT0.Day != CurSession.Day) || (MoonRiseDT0.Day != CurSession.Day))
            {
                if (MoonSetDT0.Hour == 0)
                {
                    //Console.Write("" + MoonRiseDT0.ToString("MM-dd H:m"));
                    //Console.Write("..." + MoonSetDT1.ToString("MM-dd H:m") + "");
                    MoonSet = MoonSetDT1;

                    MoonRise = MoonRiseDT0;
                }
                else if (MoonRiseDT0.Hour == 0)
                {
                    //Console.Write("" + MoonRiseDT_1.ToString("MM-dd H:m"));
                    //Console.Write("..." + MoonSetDT0.ToString("MM-dd H:m") + "");
                    MoonSet = MoonSetDT0;
                    MoonRise = MoonRiseDT_1;
                }
            }
            else
            {
                //Console.Write("" + MoonRiseDT0.ToString("MM-dd H:m"));
                //Console.Write("..." + MoonSetDT0.ToString("MM-dd H:m") + "");

                MoonSet = MoonSetDT0;
                MoonRise = MoonRiseDT0;

            }

            outMoonRise = MoonRise;
            outMoonSet = MoonSet;

        }

        /// <summary>
        /// Short overload form
        /// </summary>
        /// <param name="outMoonEvent1"></param>
        /// <param name="outMoonEvent2"></param>
        public static void getMoonEventTimesForDate(out MoonEvent outMoonEvent1, out MoonEvent outMoonEvent2)
        {
            getMoonEventTimesForDate(DateTime.Now, Latitude, Longitude, SiteTimeZone, out outMoonEvent1, out outMoonEvent2);
        }

        /// <summary>
        /// Returns moon rise data in format: 1st event (rise or set) and 2nd event (rise or set)
        /// Удобно для выводва данных (примеры есть в тексте)
        /// </summary>
        /// <param name="CurDate">Дата DateTime</param>
        /// <param name="lat">Долгота, доли градуса</param>
        /// <param name="lon">Долгота, доли градуса</param>
        /// <param name="timezoneoffset">Смещение, 3 для Москвы</param>
        /// <param name="outMoonEvent1">out Event1</param>
        /// <param name="outMoonEvent2">out Event2</param>
        public static void getMoonEventTimesForDate(DateTime CurDate, double lat, double lon, double timezoneoffset, out MoonEvent outMoonEvent1, out MoonEvent outMoonEvent2)
        {
            DateTime CurSession = CurDate.AddHours(-12);
            //int Date = ;

            DateTime MoonRiseDT0, MoonSetDT0;
            DateTime MoonRiseDT1, MoonSetDT1;
            DateTime MoonRiseDT_1, MoonSetDT_1;
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.Day, lat, lon, timezoneoffset, out MoonRiseDT0, out MoonSetDT0);
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.AddDays(1).Day, lat, lon, timezoneoffset, out MoonRiseDT1, out MoonSetDT1);
            MoonClass.calculateMoonTimes(CurSession.Year, CurSession.Month, CurSession.AddDays(-1).Day, lat, lon, timezoneoffset, out MoonRiseDT_1, out MoonSetDT_1);

            MoonEvent MoonEvent1 = new MoonEvent();
            MoonEvent MoonEvent2 = new MoonEvent();

            if (MoonSetDT0.Hour < MoonRiseDT0.Hour && (MoonSetDT0.Day == CurSession.Day) && (MoonRiseDT0.Day == CurSession.Day))
            {
                //Console.Write(" ..." + MoonSetDT0.ToString("MM-dd H:m"));
                //Console.Write("  " + MoonRiseDT0.ToString("MM-dd H:m") + "...");
                MoonEvent1.EventTime = MoonSetDT0;
                MoonEvent1.EventType = MoonEventType.MoonSet;

                MoonEvent2.EventTime = MoonRiseDT0;
                MoonEvent2.EventType = MoonEventType.MoonRise;
            }
            else if ((MoonSetDT0.Day != CurSession.Day) || (MoonRiseDT0.Day != CurSession.Day))
            {
                if (MoonSetDT0.Hour == 0)
                {
                    //Console.Write("" + MoonRiseDT0.ToString("MM-dd H:m"));
                    //Console.Write("..." + MoonSetDT1.ToString("MM-dd H:m") + "");

                    MoonEvent1.EventTime = MoonRiseDT0;
                    MoonEvent1.EventType = MoonEventType.MoonRise;

                    MoonEvent2.EventTime = MoonSetDT1;
                    MoonEvent2.EventType = MoonEventType.MoonSet;
                }
                else if (MoonRiseDT0.Hour == 0)
                {
                    //Console.Write("" + MoonRiseDT_1.ToString("MM-dd H:m"));
                    //Console.Write("..." + MoonSetDT0.ToString("MM-dd H:m") + "");

                    MoonEvent1.EventTime = MoonRiseDT_1;
                    MoonEvent1.EventType = MoonEventType.MoonRise;

                    MoonEvent2.EventTime = MoonSetDT0;
                    MoonEvent2.EventType = MoonEventType.MoonSet;

                }
            }
            else
            {
                //Console.Write("" + MoonRiseDT0.ToString("MM-dd H:m"));
                //Console.Write("..." + MoonSetDT0.ToString("MM-dd H:m") + "");

                MoonEvent1.EventTime = MoonRiseDT0;
                MoonEvent1.EventType = MoonEventType.MoonRise;

                MoonEvent2.EventTime = MoonSetDT0;
                MoonEvent2.EventType = MoonEventType.MoonSet;
            }

            outMoonEvent1 = MoonEvent1;
            outMoonEvent2 = MoonEvent2;

        }


    }

}
