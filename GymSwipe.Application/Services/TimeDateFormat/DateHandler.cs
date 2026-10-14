using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.TimeDateFormat
{
    public class DateHandler : IDateHandler
    {
        public string SetDate()
        {
            return DateTime.Now.ToString();
        }

        public DateOnly SetDateAsDateOnly()
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        public DateTime SetDateAsDateTime()
        {
            return DateTime.Now;
        }

        public string SetDateAsString()
        {
            return DateTime.Now.ToString();
        }
    }
}
