using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat
{
    public interface IDateHandler
    {
        string SetDateAsString();
        DateTime SetDateAsDateTime();
        DateOnly SetDateAsDateOnly();
    }
}
