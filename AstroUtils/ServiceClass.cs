using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsrtoUtils
{
    public static class ServiceClass
    {

        /// <summary>
        /// Service function to convert from HourDouble format into "HH:mm:ss" string format
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <returns></returns>
        static public string ConvertToTimeString(double HourDouble)
        {
            return ConvertToTimeString(HourDouble,DateTimeKind.Local,0);
        }
        static public string ConvertToTimeString(double HourDouble, DateTimeKind DateTimeKindParam)
        {
            return ConvertToTimeString(HourDouble, DateTimeKindParam, 0);
        }
        static public string ConvertToTimeString(double HourDouble, DateTimeKind DateTimeKindParam, int DayShift)
        {
            return ConvertToDateTime(HourDouble, DayShift, DateTimeKindParam).ToString("HH:mm:ss");
        }

        /// <summary>
        /// Service function to convert from HourDouble format into DateTime format
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <returns></returns>
        static public DateTime ConvertToDateTime(double HourDouble)
        {
            return ConvertToDateTime(HourDouble,0, DateTimeKind.Local);
        }
        static public DateTime ConvertToDateTime(double HourDouble, int DayShift)
        {
            return ConvertToDateTime(HourDouble, DayShift, DateTimeKind.Local);
        }
        static public DateTime ConvertToDateTime(double HourDouble,int DayShift, DateTimeKind DateTimeKindParam)
        {
            if (HourDouble < 0)
            {
                HourDouble = HourDouble + 24;
                DayShift =- 1; 
            }
            int h = (int)Math.Truncate(HourDouble);
            int m = (int)Math.Truncate((HourDouble - h) * 60);
            int s = (int)Math.Truncate((HourDouble - h - m / 60.0) * 3600);

            DateTime res;
            if (DateTimeKindParam == DateTimeKind.Utc)
            {
                res = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day + DayShift, h, m, s, DateTimeKindParam);
            }
            else
            { 
                res = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + DayShift, h, m, s, DateTimeKindParam);
            }

            return res;
        }


        /// <summary>
        /// If DataTime has specified DateTimeKind.Utc, then convert to local
        /// </summary>
        /// <param name="DT"></param>
        /// <returns></returns>
        static public DateTime ConvertToLocal(DateTime DT)
        {
            if (DT.Kind == DateTimeKind.Utc)
                return TimeZoneInfo.ConvertTimeFromUtc(DT, TimeZoneInfo.Local);
            else
                return DT;
        }



        /// <summary>
        /// Get UTC time in DateTime format
        /// </summary>
        /// <returns>DateTime UTC time</returns>
        static public DateTime GetUTCDateTime()
        {
            return DateTime.UtcNow;
        }


        /// <summary>
        /// Get UTC time in HourDouble format
        /// </summary>
        /// <returns>double UTC time</returns>
        static public double GetUTCTimeDouble()
        {
            double h = DateTime.UtcNow.Hour;
            double m = DateTime.UtcNow.Minute;
            double s = DateTime.UtcNow.Second;

            return (h + m / 60.0 + s / 3600.0);
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
