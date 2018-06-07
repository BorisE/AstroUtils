using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASCOM.Utilities;
using ASCOM.Astrometry;
using ASCOM;
using System.Collections;
using ASCOM.Astrometry.AstroUtils;
using System.Threading;
using LoggingLib;

namespace AsrtoUtils
{
    public class AstroUtilsClass
    {
        static ASCOM.Utilities.Util ASCOMUtils;
        static ASCOM.Astrometry.AstroUtils.AstroUtils ASCOMAUtils;

        // OBSERVATORY LOCATION
        static public double Latitude = 55.9452777777778;
        static public double Longitude = 38.7133333333333;
        static public double SiteTimeZone = 3;

        /// <summary>
        /// Atuo constructor
        /// </summary>
        static AstroUtilsClass()
        {
            ASCOMUtils = new Util();
            ASCOMAUtils = new AstroUtils();
        }


        /// <summary>
        /// Service function to convert from HourDouble format into "HH:mm:ss" string format
        /// </summary>
        /// <param name="HourDouble"></param>
        /// <returns></returns>
        static public string ConvertToTimeString(double HourDouble)
        {
            int h = (int)Math.Truncate(HourDouble);
            int m = (int)Math.Truncate((HourDouble - h) * 60);
            int s = (int)Math.Truncate((HourDouble - h - m / 60.0) * 3600);

            return h.ToString("D2") + ":" + m.ToString("D2") + ":" + s.ToString("D2");
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
        static public double GetUTCTime()
        {
            double h = DateTime.UtcNow.Hour;
            double m = DateTime.UtcNow.Minute;
            double s = DateTime.UtcNow.Second;

            return (h + m / 60.0 + s / 3600.0);
        }


        /// <summary>
        /// Get JulianDate
        /// </summary>
        /// <returns>Classic Julian Date</returns>
        static public double GetJD()
        {
            return ASCOMUtils.JulianDate;
        }


        /// <summary>
        /// Calculates Greenwich Apparent Sidereal time
        /// </summary>
        /// <returns>HourDouble format</returns>
        static public double NowLAST()
        {
            var nov = new ASCOM.Astrometry.NOVAS.NOVAS31();
            var ast = new ASCOM.Astrometry.AstroUtils.AstroUtils();

            var currJD = ast.JulianDateUT1(0);

            double gstNow = 0;
            var res = nov.SiderealTime(
                currJD, 0d, 0, GstType.GreenwichApparentSiderealTime, Method.CIOBased, Accuracy.Reduced, ref gstNow);

            if (res != 0) throw new InvalidValueException("Error getting Greenwich Apparent Sidereal time");

            double lstNow = gstNow + Longitude / 15;
            lstNow = lstNow - (lstNow >= 24 ? 24 : 0);

            return lstNow;
        }

        /// <summary>
        /// Calculates Greenwich Mean Sidereal time
        /// </summary>
        /// <returns>HourDouble format</returns>
        static public double NowLMST()
        {
            var nov = new ASCOM.Astrometry.NOVAS.NOVAS31();
            var ast = new ASCOM.Astrometry.AstroUtils.AstroUtils();

            var currJD = ast.JulianDateUT1(0);

            double gstNow = 0;
            var res = nov.SiderealTime(
                currJD, 0d, 0, GstType.GreenwichMeanSiderealTime, Method.EquinoxBased, Accuracy.Full, ref gstNow);

            if (res != 0) throw new InvalidValueException("Error getting Greenwich Mean Sidereal time");

            double lstNow = gstNow + Longitude / 15;
            lstNow = lstNow - (lstNow >= 24 ? 24 : 0);

            return lstNow;
        }



        /// <summary>
        /// Return Moon Set time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double MoonSet(int DayShift = 0)
        {
            ArrayList EventList = CalcMoonRiseSet(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 1) res = (double)EventList[1];

            return res;
        }

        /// <summary>
        /// Return Moon Rise time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double MoonRise(int DayShift = 0)
        {
            ArrayList EventList = CalcMoonRiseSet(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 0) res = (double)EventList[0];

            return res;

        }

        /// <summary>
        /// Caclulate Moon rise and set events
        /// </summary>
        /// <param name="DayShift">number of days to shift</param>
        /// <returns></returns>
        static public ArrayList CalcMoonRiseSet(int DayShift = 0)
        {
            ArrayList EventList = new ArrayList();
            ArrayList ReturnEventList = new ArrayList();
            try
            {
                // Get the rise and set events list
                EventList = ASCOMAUtils.EventTimes(EventType.MoonRiseMoonSet, DateTime.Now.Day + DayShift, DateTime.Now.Month, DateTime.Now.Year, Latitude, Longitude, SiteTimeZone);

                int MoonRiseCount = (int)EventList[1];
                int MoonSetCount = (int)EventList[2];

                double MoonRise = (double)EventList[2+ MoonRiseCount]; //last rise
                double MoonSet = (double)EventList[2 + MoonRiseCount+ MoonSetCount]; //last set

                ReturnEventList.Add(MoonRise);
                ReturnEventList.Add(MoonSet);

                //Logging.AddLog("MoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);

            }
            catch (ASCOM.InvalidValueException) 
            {
                // Indicates that an invalid day has been specified e.g. 31st of February so ignore it
            }
            catch (Exception ex)
            {
                Logging.AddLog("CalcMoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            if (EventList.Count > 0)
            {

            }

            return ReturnEventList;
        }


        /// <summary>
        /// Return Sun Set time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double SunSet(int DayShift = 0)
        {
            ArrayList EventList = CalcSunRiseSet(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 1) res = (double)EventList[1];

            return res;

        }

        /// <summary>
        /// Return Sun Rise time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double SunRise(int DayShift = 0)
        {
            ArrayList EventList = CalcSunRiseSet(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 0) res = (double)EventList[0];

            return res;

        }
        /// <summary>
        /// Caclulate Sun rise and set events
        /// </summary>
        /// <param name="DayShift">number of days to shift</param>
        /// <returns></returns>
        static public ArrayList CalcSunRiseSet(int DayShift = 0)
        {
            ArrayList EventList = new ArrayList();
            ArrayList ReturnEventList = new ArrayList();
            try
            {
                // Get the rise and set events list
                EventList = ASCOMAUtils.EventTimes(EventType.SunRiseSunset, DateTime.Now.Day + DayShift, DateTime.Now.Month, DateTime.Now.Year, Latitude, Longitude, SiteTimeZone);

                int SunRiseCount = (int)EventList[1];
                int SunSetCount = (int)EventList[2];

                double SunRise = (double)EventList[2 + SunRiseCount]; //last rise
                double SunSet = (double)EventList[2 + SunRiseCount + SunSetCount]; //last set

                ReturnEventList.Add(SunRise);
                ReturnEventList.Add(SunSet);

                //Logging.AddLog("MoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);

            }
            catch (ASCOM.InvalidValueException)
            {
                // Indicates that an invalid day has been specified e.g. 31st of February so ignore it
            }
            catch (Exception ex)
            {
                // Any other unexpected exception
                Logging.AddLog("CalcSunRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            if (EventList.Count > 0)
            {

            }

            return ReturnEventList;
        }



        /// <summary>
        /// Return Civil Twilight start time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double CivilTwilightRise(int DayShift = 0)
        {
            ArrayList EventList = CalcCivilTwilight(DayShift);
        
            double res = 0.0;
            if (EventList != null && EventList.Count > 0) res = (double)EventList[0];

            return res;
        }
        /// <summary>
        /// Return Civil Twilight end  time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double CivilTwilightSet(int DayShift = 0)
        {
            ArrayList EventList = CalcCivilTwilight(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 1) res = (double)EventList[1];

            return res;
        }

        /// <summary>
        /// Caclulate Civil Twilight events
        /// </summary>
        /// <param name="DayShift">number of days to shift</param>
        /// <returns></returns>
        static public ArrayList CalcCivilTwilight(int DayShift = 0)
        {
            ArrayList EventList = new ArrayList();
            ArrayList ReturnEventList = new ArrayList();
            try
            {
                // Get the rise and set events list
                EventList = ASCOMAUtils.EventTimes(EventType.CivilTwilight, DateTime.Now.Day + DayShift, DateTime.Now.Month, DateTime.Now.Year, Latitude, Longitude, SiteTimeZone);

                int RiseCount = (int)EventList[1];
                int SetCount = (int)EventList[2];

                double RiseHr = 0.0;
                if (RiseCount > 0) RiseHr = (double)EventList[2 + RiseCount]; //last rise

                double SetHr = 0.0;
                if (SetCount > 0) SetHr = (double)EventList[2 + RiseCount + SetCount]; //last set


                ReturnEventList.Add(RiseHr);
                ReturnEventList.Add(SetHr);

                //Logging.AddLog("MoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }
            catch (ASCOM.InvalidValueException)
            {
                // Indicates that an invalid day has been specified e.g. 31st of February so ignore it
            }
            catch (Exception ex)
            {
                Logging.AddLog("CalcCivilTwilight exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            if (EventList.Count > 0)
            {

            }

            return ReturnEventList;
        }

        /// <summary>
        /// Return Naut Twilight start time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double NautTwilightRise(int DayShift = 0)
        {
            ArrayList EventList = CalcNautTwilight(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 0) res = (double)EventList[0];

            return res;

        }
        /// <summary>
        /// Return Naut Twilight end  time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double NautTwilightSet(int DayShift = 0)
        {
            ArrayList EventList = CalcNautTwilight(DayShift);

            double res = 0.0;
            if (EventList != null && EventList.Count > 1) res = (double)EventList[1];

            return res;
        }
        /// <summary>
        /// Caclulate Naut Twilight events
        /// </summary>
        /// <param name="DayShift">number of days to shift</param>
        /// <returns></returns>
        static public ArrayList CalcNautTwilight(int DayShift = 0)
        {
            ArrayList EventList = new ArrayList();
            ArrayList ReturnEventList = new ArrayList();
            try
            {
                // Get the rise and set events list
                EventList = ASCOMAUtils.EventTimes(EventType.NauticalTwilight, DateTime.Now.Day + DayShift, DateTime.Now.Month, DateTime.Now.Year, Latitude, Longitude, SiteTimeZone);

                int RiseCount = (int)EventList[1];
                int SetCount = (int)EventList[2];

                double RiseHr = 0.0;
                if (RiseCount>0) RiseHr = (double)EventList[2 + RiseCount]; //last rise

                double SetHr = 0.0;
                if (SetCount > 0) SetHr = (double)EventList[2 + RiseCount + SetCount]; //last set

                ReturnEventList.Add(RiseHr);
                ReturnEventList.Add(SetHr);

                //Logging.AddLog("MoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }
            catch (ASCOM.InvalidValueException)
            {
                // Indicates that an invalid day has been specified e.g. 31st of February so ignore it
            }
            catch (Exception ex)
            {
                Logging.AddLog("CalcNautTwilight exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            if (EventList.Count > 0)
            {

            }

            return ReturnEventList;
        }


        /// <summary>
        /// Return Astron Twilight start time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double AstronTwilightRise(int DayShift = 0)
        {
            double res = 0;

            ArrayList EventList = CalcAstronTwilight(DayShift);
            //Logging.AddLog("test " + EventList.ToString(), LogLevel.Debug, Highlight.Error);
            //Thread.Sleep(3000);
            if (EventList != null && EventList.Count > 0) res = (double)EventList[0];

            return res;
        }
        /// <summary>
        /// Return Astron Twilight end  time in HourDouble
        /// Wrapper for EventTime calculations
        /// </summary>
        /// <returns></returns>
        static public double AstronTwilightSet(int DayShift = 0)
        {
            double res = 0.0;

            ArrayList EventList = CalcAstronTwilight(DayShift);
            if (EventList != null && EventList.Count > 1) res = (double)EventList[1];

            return res;
        }
        /// <summary>
        /// Caclulate Astron Twilight events
        /// </summary>
        /// <param name="DayShift">number of days to shift</param>
        /// <returns></returns>
        static public ArrayList CalcAstronTwilight(int DayShift = 0)
        {
            ArrayList EventList = new ArrayList();
            ArrayList ReturnEventList = new ArrayList();
            try
            {
                // Get the rise and set events list
                EventList = ASCOMAUtils.EventTimes(EventType.AstronomicalTwilight, DateTime.Now.Day + DayShift, DateTime.Now.Month, DateTime.Now.Year, Latitude, Longitude, SiteTimeZone);

                int RiseCount = (int)EventList[1];
                int SetCount = (int)EventList[2];

                double RiseHr = 0.0;
                if (RiseCount > 0) RiseHr = (double)EventList[2 + RiseCount]; //last rise

                double SetHr = 0.0;
                if (SetCount > 0) SetHr = (double)EventList[2 + RiseCount + SetCount]; //last set

                ReturnEventList.Add(RiseHr);
                ReturnEventList.Add(SetHr);

                //Logging.AddLog("MoonRiseSet exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }
            catch (ASCOM.InvalidValueException)
            {
                // Indicates that an invalid day has been specified e.g. 31st of February so ignore it
                Logging.AddLog("CalcAstronTwilight ASCOM.InvalidValueException! ", LogLevel.Debug, Highlight.Error);
            }
            catch (Exception ex)
            {
                Logging.AddLog("CalcAstronTwilight exception! " + ex.ToString(), LogLevel.Debug, Highlight.Error);
            }

            if (EventList.Count > 0)
            {

            }

            return ReturnEventList;
        }


        static public double MoonIllumination()
        {
            double phase = Math.Round(ASCOMAUtils.MoonIllumination(ASCOMAUtils.JulianDateUtc),2);

            return phase;
        }




























        static public double GetSideralTime_()
        {

            DateTime dateTime = DateTime.Now;

            double julianDate = 367 * dateTime.Year -
                (int)((7.0 / 4.0) * (dateTime.Year +
                (int)((dateTime.Month + 9.0) / 12.0))) +
                (int)((275.0 * dateTime.Month) / 9.0) +
                dateTime.Day - 730531.5; ;

            double julianCenturies = julianDate / 36525.0;

            // Sidereal Time
            double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

            double siderealTimeUT = siderealTimeHours +
                (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

            double siderealTime = siderealTimeUT * 15 + Longitude;

            return siderealTime;
        }


        private const double Deg2Rad = Math.PI / 180.0;
        private const double Rad2Deg = 180.0 / Math.PI;

        /*!
            * \brief Calculates the sun light.
            *
            * CalcSunPosition calculates the suns "position" based on a
            * given date and time in local time, latitude and longitude
            * expressed in decimal degrees. It is based on the method
            * found here:
            * http://www.astro.uio.no/~bgranslo/aares/calculate.html
            * The calculation is only satisfiably correct for dates in
            * the range March 1 1900 to February 28 2100.
            * \param dateTime Time and date in local time.
            * \param latitude Latitude expressed in decimal degrees.
            * \param longitude Longitude expressed in decimal degrees.
            */
        public static void CalculateSunPosition(DateTime dateTime, double latitude, double longitude)
        {
            // Convert to UTC
            dateTime = dateTime.ToUniversalTime();

            // Number of days from J2000.0.
            double julianDate = 367 * dateTime.Year -
                (int)((7.0 / 4.0) * (dateTime.Year +
                (int)((dateTime.Month + 9.0) / 12.0))) +
                (int)((275.0 * dateTime.Month) / 9.0) +
                dateTime.Day - 730531.5;

            double julianCenturies = julianDate / 36525.0;

            // Sidereal Time
            double siderealTimeHours = 6.6974 + 2400.0513 * julianCenturies;

            double siderealTimeUT = siderealTimeHours +
                (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;

            double siderealTime = siderealTimeUT * 15 + longitude;

            // Refine to number of days (fractional) to specific time.
            julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;
            julianCenturies = julianDate / 36525.0;

            // Solar Coordinates
            double meanLongitude = CorrectAngle(Deg2Rad *
                (280.466 + 36000.77 * julianCenturies));

            double meanAnomaly = CorrectAngle(Deg2Rad *
                (357.529 + 35999.05 * julianCenturies));

            double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *
                Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));

            double elipticalLongitude =
                CorrectAngle(meanLongitude + equationOfCenter);

            double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;

            // Right Ascension
            double rightAscension = Math.Atan2(
                Math.Cos(obliquity) * Math.Sin(elipticalLongitude),
                Math.Cos(elipticalLongitude));

            double declination = Math.Asin(
                Math.Sin(rightAscension) * Math.Sin(obliquity));

            // Horizontal Coordinates
            double hourAngle = CorrectAngle(siderealTime * Deg2Rad) - rightAscension;

            if (hourAngle > Math.PI)
            {
                hourAngle -= 2 * Math.PI;
            }

            double altitude = Math.Asin(Math.Sin(latitude * Deg2Rad) *
                Math.Sin(declination) + Math.Cos(latitude * Deg2Rad) *
                Math.Cos(declination) * Math.Cos(hourAngle));

            // Nominator and denominator for calculating Azimuth
            // angle. Needed to test which quadrant the angle is in.
            double aziNom = -Math.Sin(hourAngle);
            double aziDenom =
                Math.Tan(declination) * Math.Cos(latitude * Deg2Rad) -
                Math.Sin(latitude * Deg2Rad) * Math.Cos(hourAngle);

            double azimuth = Math.Atan(aziNom / aziDenom);

            if (aziDenom < 0) // In 2nd or 3rd quadrant
            {
                azimuth += Math.PI;
            }
            else if (aziNom < 0) // In 4th quadrant
            {
                azimuth += 2 * Math.PI;
            }

            // Altitude
            Console.WriteLine("Altitude: " + altitude * Rad2Deg);

            // Azimut
            Console.WriteLine("Azimuth: " + azimuth * Rad2Deg);
        }

        /*!
        * \brief Corrects an angle.
        *
        * \param angleInRadians An angle expressed in radians.
        * \return An angle in the range 0 to 2*PI.
        */
        private static double CorrectAngle(double angleInRadians)
        {
            if (angleInRadians < 0)
            {
                return 2 * Math.PI - (Math.Abs(angleInRadians) % (2 * Math.PI));
            }
            else if (angleInRadians > 2 * Math.PI)
            {
                return angleInRadians % (2 * Math.PI);
            }
            else
            {
                return angleInRadians;
            }
        }



//using ASCOM.Astrometry.Transform;
//        public void j2000toJNow(Target target)
//        {
//            this.form.log("J2000 -> JNow - in: " + target.getRa() + ", " + target.getDec());
//            Transform t = new Transform();

//            t.SiteElevation = Globals.god.telescope.SiteElevation;
//            t.SiteLatitude = Globals.god.telescope.SiteLatitude;
//            t.SiteLongitude = Globals.god.telescope.SiteLongitude;
//            t.SiteTemperature = 0;

//            t.SetJ2000(target.getRa() / 360 * 24, target.getDec());

//            target.setRa(t.RATopocentric / 24 * 360);
//            target.setDec(t.DECTopocentric);
//            this.form.log("J2000 -> JNow - out: " + target.getRa() + ", " + target.getDec());
//        }
        
    }
}
