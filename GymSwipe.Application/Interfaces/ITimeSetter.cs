using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITimeSetter
    {
        TimeSpan SetTime(Stopwatch stopwatch);
    }
}
