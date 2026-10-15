using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Display
{
    //Store errors in this handy helper
    //Print as strings
    public interface IDisplayInfoHelper
    {
        Task DisplayStandardAlert(string msg);
        Task DisplayErrorAlert(List<string> errors);
    }
}
