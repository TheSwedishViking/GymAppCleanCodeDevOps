using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Display;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {
        private List<GymPlaylistDTO> _allPlaylists = new();
        private int _currentPage = 0;
        private const int PageSize = 3;
        public ObservableCollection<GymPlaylistDTO> CurrentThreePlaylists { get; } = new(); // Ui responsive

        private string _buttonText = "Todays Message!";
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private string _greetingUser;
        public string GreetingUser
        {
            get { return _greetingUser; }
            set
            {
                _greetingUser = value;
                OnPropertyChanged(nameof(GreetingUser));
            }
        }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand GreetUserCommand { get; }
        public ICommand PlayPlaylistCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private GymUser _currentUser;
        public GymUser CurrentUser
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value) return;
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
        private readonly LoggedInUser _loggedInUser;
        private readonly IGymPlaylistService _gymPlaylistService;
        private readonly IDisplayInfoHelper _displayInfoHelper;
        public UserPageAccountViewModel(LoggedInUser loggedInUser, IGymPlaylistService gymPlaylistService, IDisplayInfoHelper displayInfoHelper)
        {
            _loggedInUser = loggedInUser;
            _displayInfoHelper = displayInfoHelper;

            _gymPlaylistService = gymPlaylistService;
            NextPageCommand = new Command(() => ChangePage(1)); //Calls method with argument
            PreviousPageCommand = new Command(() => ChangePage(-1));
            GreetUserCommand = new Command(GreetUser);  //Calls method directly

            PlayPlaylistCommand = new Command<GymPlaylistDTO>(async e => await StartThisPlayList(e));


        }
        public async Task StartThisPlayList(GymPlaylistDTO gymPlaylist)
        {
            await Shell.Current.GoToAsync(nameof(Pages.ActiveWorkoutPage), new Dictionary<string, object> //Navigation to the page with the dictionary
            {
                ["PlayListId"] = gymPlaylist.Id //pass the playlist id to the key
            });
        }


        public async Task InitalizeAsync()
        {
            CurrentUser = _loggedInUser.CurrentUser;
            await GetAllTheUsersPlaylists();

        }

        public async Task GetAllTheUsersPlaylists()
        {
            if (CurrentUser == null)
            {
                await _displayInfoHelper.DisplayStandardAlert("You're not logged in! Go log in or register");
                await Shell.Current.Navigation.PopToRootAsync();
                return;
            }
            var playlists = await _gymPlaylistService.GetPlaylistDTOsByUserId(CurrentUser.Id);
            _allPlaylists = playlists;
            _currentPage = 0;
            ShowCurrentPage();
        }

        private void ChangePage(int pDirection) // direction for pages whith min max values
        {
            _currentPage += pDirection;

            if (_currentPage < 0)
                _currentPage = 0;

            if (_currentPage * PageSize >= _allPlaylists.Count)
                _currentPage = Math.Max(0, (_allPlaylists.Count - 1) / PageSize);

            ShowCurrentPage();
        }

        private void ShowCurrentPage()
        {
            CurrentThreePlaylists.Clear();

            var playlistsToShow = _allPlaylists.Skip(_currentPage * PageSize).Take(PageSize); //Skip when multiple pages deep, take the next 3

            foreach (var playlist in playlistsToShow) //observble cant add range
            {
                CurrentThreePlaylists.Add(playlist);
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void GreetUser()
        {
            ButtonText = " = )";
            GreetingUser = $"You are looking swole today! {_loggedInUser.CurrentUser.Firstname} {_loggedInUser.CurrentUser.Id}!";
        }

    }
}

















