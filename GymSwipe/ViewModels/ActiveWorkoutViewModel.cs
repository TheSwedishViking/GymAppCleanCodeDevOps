using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ViewModels
{
    public class ActiveWorkoutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IWorkoutFacade _workoutFacade;
        private readonly  LoggedInUser _loggedInUser;
        public ActiveWorkoutViewModel(IWorkoutFacade workoutFacade, LoggedInUser loggedInUser)
        {
            _workoutFacade = workoutFacade;
            _loggedInUser = loggedInUser;
        }


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task InitalizeAsync()
        {
            await _workoutFacade.GetPlaylist(_loggedInUser.CurrentUser.Id);

        }
    }
}
