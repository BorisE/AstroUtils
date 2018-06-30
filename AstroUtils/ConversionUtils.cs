using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsrtoUtils.Conversion
{
    public static class DateTimeUtils
    {

        /// <summary>
        /// Сonvert from HourDouble format into "HH:mm:ss" string format
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <returns></returns>
        static public string ConvertToTimeString(double HourDouble)
        {
            return ConvertToTimeString(HourDouble, DateTimeKind.Local, 0);
        }
        static public string ConvertToTimeString(double HourDouble, DateTimeKind DateTimeKindParam)
        {
            return ConvertToTimeString(HourDouble, DateTimeKindParam, 0);
        }
        /// <summary>
        /// Сonvert from HourDouble format into "HH:mm:ss" string format
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <param name="DateTimeKindParam"></param>
        /// <param name="DayShift"></param>
        /// <returns></returns>
        static public string ConvertToTimeString(double HourDouble, DateTimeKind DateTimeKindParam, int DayShift)
        {
            return ConvertToDateTime(HourDouble, DayShift, DateTimeKindParam).ToString("HH:mm:ss");
        }

        /// <summary>
        /// Convert from HourDouble format into DateTime format
        /// Assume time is local time
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <returns></returns>
        static public DateTime ConvertToDateTime(double HourDouble)
        {
            return ConvertToDateTime(HourDouble, 0, DateTimeKind.Local);
        }

        /// <summary>
        /// Convert from HourDouble format into DateTime format with given Day shift
        /// Assume time is local time
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <param name="DayShift">Shift in days (+ next, - prev)</param>
        /// <returns></returns>
        static public DateTime ConvertToDateTime(double HourDouble, int DayShift)
        {
            return ConvertToDateTime(HourDouble, DayShift, DateTimeKind.Local);
        }
        /// <summary>
        /// Convert from HourDouble format into DateTime format with given Day shift
        /// and specifed UTC or Local time
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <param name="DayShift"></param>
        /// <param name="DateTimeKindParam">DateTimeKind.Utc | DateTimeKind.Local</param>
        /// <returns></returns>
        static public DateTime ConvertToDateTime(double HourDouble, int DayShift, DateTimeKind DateTimeKindParam)
        {
            if (HourDouble < 0)
            {
                HourDouble = HourDouble + 24;
                DayShift = -1;
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

        /// <summary>
        /// Convert from UNIX Timestamp to DateTime.
        /// Would be in UTC
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// Convert DateTime to UNIX timestamp
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }


    /* ***************************************************************************
     * Conversion utlils
     * **************************************************************************
     */
    public static class DoubleConversionUtils
    {
        /// <summary>
        /// Convert from string to double, but check for alternative separator
        /// </summary>
        /// <param name="Val">double in string format</param>
        /// <returns>double value</returns>
        public static double ConvertToDouble(string Val)
        {
            double DblRes = double.MinValue;
            //1. Try to convert
            if (double.TryParse(Val, out DblRes))
            {
                return DblRes;
            }
            else
            {
                //2.1. Automatic decimal point correction
                char Separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                char BadSeparator = '.';

                if (Separator == '.') { BadSeparator = ','; }
                if (Separator == ',') { BadSeparator = '.'; }

                string Val_st = Val.Replace(BadSeparator, Separator);

                //2.2. Try to convert to double. 
                try
                {
                    DblRes = Convert.ToDouble(Val_st);
                }
                catch (Exception Ex)
                {
                    throw;
                }

                return DblRes;
            }
        }
        public static bool TryParseToDouble(string Val, out double DblRes)
        {
            DblRes = double.MinValue;
            //1. Try to convert
            if (double.TryParse(Val, out DblRes))
            {
                return true;
            }
            else
            {
                //2.1. Automatic decimal point correction
                char Separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                char BadSeparator = '.';

                if (Separator == '.') { BadSeparator = ','; }
                if (Separator == ',') { BadSeparator = '.'; }

                string Val_st = Val.Replace(BadSeparator, Separator);

                //2.2. Try to convert to double. 
                try
                {
                    DblRes = Convert.ToDouble(Val_st);
                    return true;
                }
                catch (Exception Ex)
                {
                    return false;
                }

                return true;
            }
        }

    }



}
