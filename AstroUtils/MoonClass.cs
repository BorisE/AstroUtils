using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroUtils
{
    public class HourMinute
    {
        public int Hour = -1;
        public int Minute = -1;
    }

    public static class MoonClass
    {

        /**
             * Calculates the moon rise/set for a given location and day of year
             * 
             * calculateMoonTimes(2018, 07, 24, 44.7913, 38.5835, 3, out MoonRiseDT, out MoonSetDT)
             * 
        */
        public static void calculateMoonTimes(int year, int month, int day, double lat, double lon, double timezoneoffset, out DateTime moonrise, out DateTime moonset)
        {
            double utrise, utset;

            utrise = utset = 0;

            //$timezone = (int)($lon / 15); 
            int timezone = (int)timezoneoffset;

            double date = modifiedJulianDate(month, day, year);


            date -= timezone / 24.0;
            double latRad = DegreeToRadian(lat);
            double sinho = 0.0023271056;
            double sglat = Math.Sin(latRad);
            double cglat = Math.Cos(latRad);

            bool rise = false;
            bool set = false;
            bool above = false;
            int hour = 1;
            double ym = sinAlt(date, hour - 1, lon, cglat, sglat) - sinho;

            above = ym > 0;
            while (hour < 25 && (false == set || false == rise))
            {
                double yz = sinAlt(date, hour, lon, cglat, sglat) - sinho;
                double yp = sinAlt(date, hour + 1, lon, cglat, sglat) - sinho;

                Tuple<double, double, double, double, double> quadout = quad(ym, yz, yp);
                double nz = quadout.Item1;
                double z1 = quadout.Item2;
                double z2 = quadout.Item3;
                double xe = quadout.Item4;
                double ye = quadout.Item5;

                if (nz == 1)
                {
                    if (ym < 0)
                    {
                        utrise = hour + z1;
                        rise = true;
                    }
                    else
                    {
                        utset = hour + z1;
                        set = true;
                    }
                }

                if (nz == 2)
                {
                    if (ye < 0)
                    {
                        utrise = hour + z2;
                        utset = hour + z1;
                    }
                    else
                    {
                        utrise = hour + z1;
                        utset = hour + z2;
                    }
                }

                ym = yp;
                hour += 2;
            }

            // Convert to unix timestamps and return as an object
            HourMinute utriseHM = convertTime(utrise);
            HourMinute utsetHM = convertTime(utset);

            moonrise = rise ? new DateTime(year, month, day, utriseHM.Hour, utriseHM.Minute, 0) : new DateTime(year, month, day + 1, 0, 0, 0);
            moonset = set ? new DateTime(year, month, day, utsetHM.Hour, utsetHM.Minute, 0) : new DateTime(year, month, day + 1, 0, 0, 0);
        }


        /**
         *	this rather mickey mouse function takes a lot of
        *  arguments and then returns the sine of the altitude of the moon
        */
        private static double sinAlt(double mjd, int hour, double glon, double cglat, double sglat)
        {

            mjd += hour / 24.0;
            double t = (mjd - 51544.5) / 36525;
            Tuple<double, double> objpos = minimoon(t);

            double ra = objpos.Item2;
            double dec = objpos.Item1;
            double decRad = DegreeToRadian(dec);
            double tau = 15 * (lmst(mjd, glon) - ra);

            double retVal = sglat * Math.Sin(decRad) + cglat * Math.Cos(decRad) * Math.Cos(DegreeToRadian(tau));

            return retVal;
        }


        /**
         * takes t and returns the geocentric ra and dec in an array mooneq
         * claimed good to 5' (angle) in ra and 1' in dec
         * tallies with another approximate method and with ICE for a couple of dates
         */
        private static Tuple<double, double> minimoon(double t)
        {

            double p2 = 6.283185307;
            double arc = 206264.8062;
            double coseps = 0.91748;
            double sineps = 0.39778;

            double lo = Frac(0.606433 + 1336.855225 * t);
            double l = p2 * Frac(0.374897 + 1325.552410 * t);
            double l2 = l * 2;
            double ls = p2 * Frac(0.993133 + 99.997361 * t);
            double d = p2 * Frac(0.827361 + 1236.853086 * t);
            double d2 = d * 2;
            double f = p2 * Frac(0.259086 + 1342.227825 * t);
            double f2 = f * 2;

            double sinls = Math.Sin(ls);
            double sinf2 = Math.Sin(f2);

            double dl = 22640 * Math.Sin(l);
            dl += -4586 * Math.Sin(l - d2);
            dl += 2370 * Math.Sin(d2);
            dl += 769 * Math.Sin(l2);
            dl += -668 * sinls;
            dl += -412 * sinf2;
            dl += -212 * Math.Sin(l2 - d2);
            dl += -206 * Math.Sin(l + ls - d2);
            dl += 192 * Math.Sin(l + d2);
            dl += -165 * Math.Sin(ls - d2);
            dl += -125 * Math.Sin(d);
            dl += -110 * Math.Sin(l + ls);
            dl += 148 * Math.Sin(l - ls);
            dl += -55 * Math.Sin(f2 - d2);

            double s = f + (dl + 412 * sinf2 + 541 * sinls) / arc;
            double h = f - d2;
            double n = -526 * Math.Sin(h);
            n += 44 * Math.Sin(l + h);
            n += -31 * Math.Sin(-l + h);
            n += -23 * Math.Sin(ls + h);
            n += 11 * Math.Sin(-ls + h);
            n += -25 * Math.Sin(-l2 + f);
            n += 21 * Math.Sin(-l + f);

            double L_moon = p2 * Frac(lo + dl / 1296000);
            double B_moon = (18520.0 * Math.Sin(s) + n) / arc;

            double cb = Math.Cos(B_moon);
            double x = cb * Math.Cos(L_moon);
            double v = cb * Math.Sin(L_moon);
            double w = Math.Sin(B_moon);
            double y = coseps * v - sineps * w;
            double z = sineps * v + coseps * w;
            double rho = Math.Sqrt(1 - z * z);
            double dec = (360 / p2) * Math.Atan(z / rho);
            double ra = (48 / p2) * Math.Atan(y / (x + rho));
            ra = ra < 0 ? ra + 24 : ra;

            Tuple<double, double> DECRA = new Tuple<double, double>(dec, ra);

            return DECRA;

        }


        /**
         * Takes the day, month, year and hours in the day and returns the
         * modified julian day number defined as mjd = jd - 2400000.5
         * checked OK for Greg era dates - 26th Dec 02
         */
        private static int modifiedJulianDate(int month, int day, int year)
        {

            if (month <= 2)
            {
                month += 12;
                year--;
            }

            double a = 10000 * year + 100 * month + day;
            double b = 0;
            if (a <= 15821004.1)
            {
                b = -2 * (int)((year + 4716) / 4) - 1179;
            }
            else
            {
                b = (int)(year / 400) - (int)(year / 100) + (int)(year / 4);
            }

            a = 365 * year - 679004;
            return (int)(a + b + (int)(30.6001 * (month + 1)) + day);
        }


        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static double Frac(double value)
        {
            return value - Math.Truncate(value);
        }


        /**
	     *	finds the parabola throuh the three points (-1,ym), (0,yz), (1, yp)
	     *  and returns the coordinates of the max/min (if any) xe, ye
	     *  the values of x where the parabola crosses zero (roots of the self::quadratic)
	     *  and the number of roots (0, 1 or 2) within the interval [-1, 1]
	     *
	     *	well, this routine is producing sensible answers
	     *
	     *  results passed as array [nz, z1, z2, xe, ye]
	     */
        private static Tuple<double, double, double, double, double> quad(double ym, double yz, double yp)
        {
            double nz, z1, z2;
            nz = z1 = z2 = 0;
            double a = 0.5 * (ym + yp) - yz;
            double b = 0.5 * (yp - ym);
            double c = yz;
            double xe = -b / (2 * a);
            double ye = (a * xe + b) * xe + c;
            double dis = b * b - 4 * a * c;
            if (dis > 0)
            {
                double dx = 0.5 * Math.Sqrt(dis) / Math.Abs(a);
                z1 = xe - dx;
                z2 = xe + dx;
                nz = Math.Abs(z1) < 1 ? nz + 1 : nz;
                nz = Math.Abs(z2) < 1 ? nz + 1 : nz;
                z1 = z1 < -1 ? z2 : z1;
            }

            return new Tuple<double, double, double, double, double>(nz, z1, z2, xe, ye);

        }


        /**
         * Converts an hours decimal to hours and minutes
         */
        private static HourMinute convertTime(double hours)
        {

            double hrs = (int)(hours * 60 + 0.5) / 60.0;
            int h = (int)(hrs);
            int m = (int)(60 * (hrs - h) + 0.5);


            return new HourMinute() { Hour = h, Minute = m };
        }

        private static double lmst(double mjd, double glon)
        {
            double d = mjd - 51544.5;
            double t = d / 36525;
            double lst = degRange(280.46061839 + 360.98564736629 * d + 0.000387933 * t * t - t * t * t / 38710000);
            return lst / 15 + glon / 15;
        }

        /**
         *	returns an angle in degrees in the range 0 to 360
         */
        private static double degRange(double x)
        {
            double b = x / 360;
            double a = 360 * (b - (int)b);
            double retVal = a < 0 ? a + 360 : a;
            return retVal;
        }


    }
}
