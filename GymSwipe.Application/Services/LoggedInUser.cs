using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class LoggedInUser:INotifyPropertyChanged
    {
        private GymUser _user;

        public GymUser CurrentUser
        {
            get { return _user; }
            set { _user = value; PropertyChanged?.Invoke(this, new(nameof(CurrentUser))); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
