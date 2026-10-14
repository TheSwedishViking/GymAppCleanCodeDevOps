using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IDateHandler
    {
        string SetDateAsString();
        DateTime SetDateAsDateTime();
        DateOnly SetDateAsDateOnly();
    }
}
