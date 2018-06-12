using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsrtoUtils;

namespace AsrtoUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            //ASCOM
            Console.WriteLine("ASCOM Sunset: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.SunSet()));
            Console.WriteLine("ASCOM civil end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.CivilTwilightSet()));
            Console.WriteLine("ASCOM nav end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.NautTwilightSet()));
            Console.WriteLine("ASCOM astro end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.AstronTwilightSet()));
            Console.WriteLine("ASCOM astro start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.AstronTwilightRise()));
            Console.WriteLine("ASCOM nav start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.NautTwilightRise()));
            Console.WriteLine("ASCOM civil start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.CivilTwilightRise()));
            Console.WriteLine("ASCOM Sunrise: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.SunRise()));

            //SunSet.C
            double tsunrise, tsunset;
            string sunsetTimeString;
            TimeSpan sunsetTime;

            Sunriset.AstronomicalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, AstroUtilsASCOM.Latitude, AstroUtilsASCOM.Longitude, out tsunrise, out tsunset);
            sunsetTime = TimeSpan.FromHours(tsunrise);
            sunsetTimeString = sunsetTime.ToString(@"hh\:mm\:ss");
            Console.WriteLine("AstoTwilightBeg:" + tsunrise + " " + sunsetTime + " " + sunsetTimeString);

            //SunSet wrapper
            //DateTime test1 = AstroUtilsProp.CivilTwilightRiseDateTime();
            //DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(test1, TimeZoneInfo.Local);
            //Console.WriteLine("Civil beg:" + test1.ToString("HH:mm:ss") + "->" + targetTime.ToString("HH:mm:ss"));

            Console.WriteLine("Sunset: " + AstroUtilsProp.SunSetDateTime());
            Console.WriteLine("Civil end: " + AstroUtilsProp.CivilTwilightSetDateTime());
            Console.WriteLine("nav end: " + AstroUtilsProp.NautTwilightSetDateTime());
            Console.WriteLine("astro end: " + AstroUtilsProp.AstronTwilightSetDateTime());
            Console.WriteLine("astro start: " + AstroUtilsProp.AstronTwilightRiseDateTime());
            Console.WriteLine("nav start: " + AstroUtilsProp.NautTwilightRiseDateTime());
            Console.WriteLine("civil start: " + AstroUtilsProp.CivilTwilightRiseDateTime());
            Console.WriteLine("Sunrise: " + AstroUtilsProp.SunRiseDateTime());


            Console.ReadLine();
        }
    }
}
