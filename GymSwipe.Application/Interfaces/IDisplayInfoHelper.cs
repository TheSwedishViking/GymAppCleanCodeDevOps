using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IDisplayInfoHelper
    {
        Task DisplayStandardAlert(string msg);
        Task DisplayErrorAlert(List<string> errors);
    }
}
