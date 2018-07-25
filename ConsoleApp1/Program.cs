using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsrtoUtils;
using AstroUtils;

namespace AsrtoUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            ////ASCOM
            //Console.WriteLine("ASCOM Sunset: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.SunSet()));
            //Console.WriteLine("ASCOM civil end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.CivilTwilightSet()));
            //Console.WriteLine("ASCOM nav end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.NautTwilightSet()));
            //Console.WriteLine("ASCOM astro end: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.AstronTwilightSet()));
            //Console.WriteLine("ASCOM astro start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.AstronTwilightRise()));
            //Console.WriteLine("ASCOM nav start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.NautTwilightRise()));
            //Console.WriteLine("ASCOM civil start: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.CivilTwilightRise()));
            //Console.WriteLine("ASCOM Sunrise: " + AstroUtilsASCOM.ConvertToTimeString(AstroUtilsASCOM.SunRise()));

            ////SunSet.C
            //double tsunrise, tsunset;
            //string sunsetTimeString;
            //TimeSpan sunsetTime;

            //Sunriset.AstronomicalTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, AstroUtilsASCOM.Latitude, AstroUtilsASCOM.Longitude, out tsunrise, out tsunset);
            //sunsetTime = TimeSpan.FromHours(tsunrise);
            //sunsetTimeString = sunsetTime.ToString(@"hh\:mm\:ss");
            //Console.WriteLine("AstoTwilightBeg:" + tsunrise + " " + sunsetTime + " " + sunsetTimeString);

            ////SunSet wrapper
            ////DateTime test1 = AstroUtilsProp.CivilTwilightRiseDateTime();
            ////DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(test1, TimeZoneInfo.Local);
            ////Console.WriteLine("Civil beg:" + test1.ToString("HH:mm:ss") + "->" + targetTime.ToString("HH:mm:ss"));

            //Console.WriteLine("Sunset: " + AstroUtilsProp.SunSetDateTime());
            //Console.WriteLine("Civil end: " + AstroUtilsProp.CivilTwilightSetDateTime());
            //Console.WriteLine("nav end: " + AstroUtilsProp.NautTwilightSetDateTime());
            //Console.WriteLine("astro end: " + AstroUtilsProp.AstronTwilightSetDateTime());
            //Console.WriteLine("astro start: " + AstroUtilsProp.AstronTwilightRiseDateTime());
            //Console.WriteLine("nav start: " + AstroUtilsProp.NautTwilightRiseDateTime());
            //Console.WriteLine("civil start: " + AstroUtilsProp.CivilTwilightRiseDateTime());
            //Console.WriteLine("Sunrise: " + AstroUtilsProp.SunRiseDateTime());


            //for(int i=1; i<=31; i++)
            //{ 
            //    DateTime MoonRise, MoonSet = DateTime.MinValue;
            //    AstroUtilsProp.getMoonTimesForDate(new DateTime(2018,07,i, 22, 01, 01), 44.7, 38.6, 3, out MoonRise, out MoonSet);
            //    Console.WriteLine("MR: "+MoonRise + "   MS: " + MoonSet);
            //}

            //for (int i = 1; i <= 31; i++)
            //{
            //    MoonEvent MoonEvent1 = new MoonEvent(), MoonEvent2 = new MoonEvent();
            //    AstroUtilsProp.getMoonEventTimesForDate(new DateTime(2018, 07, i, 22, 01, 01), 44.7, 38.6, 3, out MoonEvent1, out MoonEvent2);
            //    Console.WriteLine("[" + MoonEvent1.EventType.ToString() + "]: "+ MoonEvent1.EventTime + "   [" + MoonEvent2.EventType.ToString() + "]: " + MoonEvent2.EventTime );
            //}

            //for (int i = 1; i <= 31; i++)
            //{
            //    DateTime CurDate = new DateTime(2018, 07, i, 22, 01, 01);
            //    DateTime MoonRise = new DateTime(), MoonSet = new DateTime();
            //    AstroUtilsProp.getMoonTimesForSession(CurDate, 44.7, 38.6, 3, out MoonRise, out MoonSet);
            //    Console.WriteLine(CurDate + "  MR: " + MoonRise + "   MS: " + MoonSet);
            //}


            DateTime CurDate = new DateTime(2018, 07, 31, 22, 01, 01);
            DateTime MoonRise = new DateTime(), MoonSet = new DateTime();
            AstroUtilsProp.getMoonTimesForSession(CurDate, 44.7, 38.6, 3, out MoonRise, out MoonSet);
            Console.WriteLine(CurDate + "  MR: " + MoonRise + "   MS: " + MoonSet);

            Console.ReadLine();
        }
    }
}
