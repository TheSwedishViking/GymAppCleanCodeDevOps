using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.TimeDateFormat
{
    public class TimeSetter : ITimeSetter
    {
        public TimeSpan SetTime(Stopwatch stopwatch)
        {
            return TimeSpan.FromMinutes(stopwatch.Elapsed.TotalMinutes);
        }

      
    }
}
