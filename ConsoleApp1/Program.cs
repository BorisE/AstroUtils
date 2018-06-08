using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsrtoUtils;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //ASCOM
            Console.WriteLine("ASCOM Sunset: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.SunSet()));
            Console.WriteLine("ASCOM civil end: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.CivilTwilightSet()));
            Console.WriteLine("ASCOM nav end: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.NautTwilightSet()));
            Console.WriteLine("ASCOM astro end: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.AstronTwilightSet()));
            Console.WriteLine("ASCOM astro start: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.AstronTwilightRise()));
            Console.WriteLine("ASCOM nav start: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.NautTwilightRise()));
            Console.WriteLine("ASCOM civil start: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.CivilTwilightRise()));
            Console.WriteLine("ASCOM Sunrise: " + AstroUtilsClass.ConvertToTimeString(AstroUtilsClass.SunRise()));

            //SunSet.C
            //double tsunrise, tsunset;
            //string sunsetTimeString;
            //TimeSpan sunsetTime;

            //Sunriset.CivilTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, AstroUtilsClass.Latitude, AstroUtilsClass.Longitude, out tsunrise, out tsunset);
            //sunsetTime = TimeSpan.FromHours(tsunrise);
            //sunsetTimeString = sunsetTime.ToString(@"hh\:mm\:ss");
            //Console.WriteLine("CivilTwilightBeg:" + tsunrise + " " + sunsetTime + " " + sunsetTimeString);

            //SunSet wrapper
            DateTime test1 = AstroUtilsProp.CivilTwilightRiseDateTime();
            DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(test1, TimeZoneInfo.Local);
            Console.WriteLine("Civil beg:" + test1.ToString("HH:mm:ss") + "->" + targetTime.ToString("HH:mm:ss"));

            Console.ReadLine();
        }
    }
}
