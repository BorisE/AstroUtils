using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AstroUtils
{
    // Пока не используется, так как absract и static не сочитаются
    abstract class AstroUtilsAbstract
    {
        abstract public DateTime SunSetDateTime(int DayShift = 0);
        abstract public DateTime SunRiseDateTime(int DayShift = 0);


        abstract public DateTime CivilTwilightSetDateTime(int DayShift = 0);

        abstract public DateTime CivilTwilightRiseDateTime(int DayShift = 0);


        abstract public DateTime NautTwilightSetDateTime(int DayShift = 0);

        abstract public DateTime NautTwilightRiseDateTime(int DayShift = 0);

        abstract public DateTime AstronTwilightSetDateTime(int DayShift = 0);

        abstract public DateTime AstronTwilightRiseDateTime(int DayShift = 0);
    }
}
