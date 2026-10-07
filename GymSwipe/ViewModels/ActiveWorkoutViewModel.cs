using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
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
        private GymPlaylist _playList;
        public GymPlaylist GymPlaylist
        {
            get { return _playList; }
            set
            {
                _playList = value;
                OnPropertyChanged(nameof(GymPlaylist));
            }
        }
        private PlaylistExcercise _currentExercise;
        public PlaylistExcercise CurrentExercise
        {
            get { return _currentExercise; }
            set
            {
                if(_currentExercise == value) return;
                _currentExercise = value;
                OnPropertyChanged(nameof(_currentExercise));
            }
        }
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
            GymPlaylist = await _workoutFacade.GetPlaylist(_loggedInUser.CurrentUser.Id);
            Console.WriteLine(GymPlaylist);
        }
    }
}
