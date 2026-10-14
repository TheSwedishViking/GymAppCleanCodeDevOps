using GymSwipe.ApplicationLayer.Interfaces.Display;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.UI_Helper
{
    public class DisplayInfoHelper : IDisplayInfoHelper
    {
        public async Task DisplayErrorAlert(List<string> errors)
        {
            string completeMsg = string.Join("\n", errors);
            await Shell.Current.DisplayAlertAsync("Error", completeMsg, "OK");
        }

        public async Task DisplayStandardAlert(string msg)
        {
            await Shell.Current.DisplayAlertAsync("Notice", msg, "OK");
        }
    }
}
