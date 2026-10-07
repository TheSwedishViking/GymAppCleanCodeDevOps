using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IDateHandler
    {
        Task<string> SetDateAsString();
        Task<DateTime> SetDateAsDateTime();
        Task<DateOnly> SetDateAsDateOnly();
    }
}
