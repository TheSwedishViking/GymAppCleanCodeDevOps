using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Display
{
    public interface IDisplayInfoHelper
    {
        Task DisplayStandardAlert(string msg);
        Task DisplayErrorAlert(List<string> errors);
    }
}
