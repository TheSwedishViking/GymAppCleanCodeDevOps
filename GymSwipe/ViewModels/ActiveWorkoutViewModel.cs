using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    //IQueraAttributable for loading a playlist if ID provided from other page 
    public class ActiveWorkoutViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IWorkoutFacade _workoutFacade;
        private readonly  LoggedInUser _loggedInUser;
        private int? _playListId;
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _playListId = query.TryGetValue("PlayListId", out var value) && value is int id ? id : null;
        }
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
                OnPropertyChanged(nameof(CurrentExercise));
            }
        }

        public ICommand StartPlaylistCommand { get; }
        public ICommand NextExerciseCommand { get; }
        public ICommand PausePlaylistCommand {  get; }
        public ICommand PreviousExerciseCommand { get; }
        public ActiveWorkoutViewModel(IWorkoutFacade workoutFacade, LoggedInUser loggedInUser)
        {
            _workoutFacade = workoutFacade;
            _loggedInUser = loggedInUser;

            StartPlaylistCommand = new Command(async () =>await StartPlaylist());
            NextExerciseCommand = new Command(async () => await NextExercise());
            PausePlaylistCommand = new Command(async () => await PausePlaylist());
            PreviousExerciseCommand = new Command(async () => await PreviousExercise());

        }
        public async Task StartPlaylist()
        {
            CurrentExercise =  await _workoutFacade.StartWorkout();
            Console.WriteLine(CurrentExercise);
        }
        public async Task NextExercise() 
        {
            CurrentExercise = await _workoutFacade.GetNextExercise(CurrentExercise);
            if(CurrentExercise == null)
            {
                await _workoutFacade.RecordUserWorkout();
                var saveStatus = await _workoutFacade.QuitWorkout();
                if(saveStatus == true)
                {
                    await Shell.Current.Navigation.PopToRootAsync();
                }
            }
        }
        public async Task PausePlaylist() 
        {
            throw new NotImplementedException("Not implmented");
        }
        public async Task PreviousExercise()
        {
            CurrentExercise = await _workoutFacade.GetPreviousExercise(CurrentExercise);
        }
        public async Task UpdateExercise()
        {

        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task InitalizeAsync()
        {
            //
            if(_loggedInUser.CurrentUser == null)
            {
                //Pop up warning later
                await Task.Delay(1000);
                await Shell.Current.GoToAsync(nameof(Pages.UserRegistrationPage));
                return;
            }
            if(_playListId is int id)
            {
                GymPlaylist = await _workoutFacade.GetPlaylistById(id);
            }
            else
            {
                GymPlaylist = await _workoutFacade.GetPlaylistByUserId(_loggedInUser.CurrentUser.Id);
            }
          
            Console.WriteLine(GymPlaylist);
        }

    
    }
}
