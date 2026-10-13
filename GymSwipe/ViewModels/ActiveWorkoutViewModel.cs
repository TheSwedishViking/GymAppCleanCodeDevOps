using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using GymSwipe.Pages;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    //IQueraAttributable for loading a playlist if ID provided from other page 
    public class ActiveWorkoutViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IWorkoutFacade _workoutFacade;
        private readonly LoggedInUser _loggedInUser;
        private int? _playListId; //The playlist ID passed from the previous page, if any

        //Recived data from other page
        public void ApplyQueryAttributes(IDictionary<string, object> sentPlayListQuery)
        {
            _playListId = sentPlayListQuery.TryGetValue("PlayListId", out var value) && value is int id ? id : null; //id passed over
        }

        private GymPlaylistDTO _playList;
        public GymPlaylistDTO GymPlaylist
        {
            get { return _playList; }
            set
            {
                _playList = value;
                OnPropertyChanged(nameof(GymPlaylist));
            }
        }
        private PlaylistExerciseDTO _currentExercise;
        public PlaylistExerciseDTO CurrentExercise
        {
            get { return _currentExercise; }
            set
            {
                if (_currentExercise == value) return;
                _currentExercise = value;
                OnPropertyChanged(nameof(CurrentExercise));
            }
        }

        private string _cardImageSource;
        public string CardImageSource
        {
            get => _cardImageSource;
            set
            {
                if (_cardImageSource == value) return;
                _cardImageSource = value;
                OnPropertyChanged(nameof(CardImageSource));
            }
        }

        private ExerciseRecord _currentRecord;
        public ExerciseRecord CurrentRecords
        {
            get { return _currentRecord; }
            set
            {
                if (_currentRecord == value) return;
                _currentRecord = value;
                OnPropertyChanged(nameof(CurrentRecords));
            }
        }

        public bool UserIsEvaulatingGymplaylistExercise { get; set; }
        public ICommand StartPlaylistCommand { get; }
        public ICommand NextExerciseCommand { get; }
        public ICommand PausePlaylistCommand { get; }
        public ICommand PreviousExerciseCommand { get; }
        public ActiveWorkoutViewModel(IWorkoutFacade workoutFacade, LoggedInUser loggedInUser)
        {
            _workoutFacade = workoutFacade;
            _loggedInUser = loggedInUser;

            StartPlaylistCommand = new Command(async () => await StartPlaylist());
            NextExerciseCommand = new Command(async () => await NextExercise());
            PausePlaylistCommand = new Command(async () => await PausePlaylist());
            PreviousExerciseCommand = new Command(async () => await PreviousExercise());

        }
        public async Task StartPlaylist()
        {
            CurrentExercise = await _workoutFacade.StartWorkout();
            Console.WriteLine(CurrentExercise);
        }
        public async Task NextExercise()
        {
            CurrentExercise = await _workoutFacade.GetNextExercise(CurrentExercise);
            //When done
            if (CurrentExercise == null)
            {
                await _workoutFacade.CompleteWorkout();
                await Task.Yield();
                await Shell.Current.GoToAsync(nameof(ExerciseEvaulationPage), new Dictionary<string, object> { ["PlaylistId"] = GymPlaylist.Id });
                return;
            }
            CardImageSource = await _workoutFacade.GetApproptiateImageForExercise(CurrentExercise.ExerciseId);

        }
        public async Task PausePlaylist()
        {
            throw new NotImplementedException("Not implmented");
        }
        public async Task PreviousExercise()
        {
            CurrentExercise = await _workoutFacade.GetPreviousExercise(CurrentExercise);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task InitalizeAsync()
        {
            //
            if (_loggedInUser.CurrentUser == null)
            {
                //Pop up warning later
                await Task.Delay(1000);
                await Shell.Current.GoToAsync(nameof(Pages.UserRegistrationPage));
                return;
            }
            if (_playListId is int id)
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
