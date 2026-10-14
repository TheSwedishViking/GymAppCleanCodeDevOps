using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class TimeSetter : ITimeSetter
    {
        public TimeSpan SetTime(Stopwatch stopwatch)
        {
            return TimeSpan.FromMinutes(stopwatch.Elapsed.TotalMinutes);
        }

      
    }
}
