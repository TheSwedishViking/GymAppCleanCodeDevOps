using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class DateHandler : IDateHandler
    {
        public async Task<string> SetDate()
        {
            return DateTime.Now.ToString();
        }

        public async Task<DateOnly> SetDateAsDateOnly()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        public async Task<DateTime> SetDateAsDateTime()
        {
            return DateTime.UtcNow;
        }

        public async Task<string> SetDateAsString()
        {
            return DateTime.UtcNow.ToString();
        }
    }
}
