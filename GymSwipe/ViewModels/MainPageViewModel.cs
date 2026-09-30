using GymSwipe.ApplicationLayer.Services;
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
        private LoggedInUser _loggedInUser;

        private string _firstname => string.IsNullOrEmpty(CurrentUser?.Firstname) ? CurrentUser?.Firstname : "Not logged in";
        public string Firstname
        {
            get => _firstname;
            set
            {
                if (_firstname == value) return;
                CurrentUser.Firstname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Firstname)));
            }
        }
     
        public MainPageViewModel(LoggedInUser loggedInUser)
        {
            _loggedInUser = loggedInUser;
            CurrentUser = _loggedInUser.CurrentUser;
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
