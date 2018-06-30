using AsrtoUtils.Conversion;
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

    }

}
