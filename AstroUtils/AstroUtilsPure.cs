using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroUtils
{
    class AstroUtilsPure
    {
        static public DateTime NautTwilightRiseDateTime(int DayShift = 0)
        {
            Sunriset.CivilTwilight(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, AstroUtilsClass.Latitude, AstroUtilsClass.Longitude, out tsunrise, out tsunset);
            sunsetTime = TimeSpan.FromHours(tsunset);
            sunsetTimeString = sunsetTime.ToString(@"hh\:mm\:ss");

        }


    }
}
