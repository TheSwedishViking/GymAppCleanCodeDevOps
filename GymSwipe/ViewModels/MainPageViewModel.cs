using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _usersName;
        public string UsersName
        {
            get { return _usersName; }
            set
            {
                _usersName = value;
                OnPropertyChanged(nameof(UsersName));
            }
        }
        //Reload page where name gets new value
        public GymUser CurrentUser { get; set; }
        public LoggedInUser Session { get; }
     
        public MainPageViewModel(LoggedInUser loggedInUser)
        {
            Session = loggedInUser;
            CurrentUser = Session.CurrentUser;
        }
        public void UpdateLabelGreetUser()
        {

        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
