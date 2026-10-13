using GymSwipe.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.ApplicationLayer.DTOs;


namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {
        private List<GymPlaylistDTO> _allPlaylists = new();
        private int _currentPage = 0;
        private const int PageSize = 3;
        public ObservableCollection<GymPlaylistDTO> CurrentThreePlaylists { get; } = new();

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
        public ICommand DeleteUserCommand { get; }
        public ICommand PlayPlaylistCommand {  get; }
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
        private readonly HttpClient _httpClient;
        private readonly IGymPlaylistService _gymPlaylistService;
        public UserPageAccountViewModel(LoggedInUser loggedInUser, HttpClient httpClient, IGymPlaylistService gymPlaylistService)
        {
            _loggedInUser = loggedInUser;
            _httpClient = httpClient;
           
            _gymPlaylistService = gymPlaylistService;
            NextPageCommand = new Command(() => ChangePage(1));
            PreviousPageCommand = new Command(() => ChangePage(-1));
            GreetUserCommand = new Command(GreetUser);
            DeleteUserCommand = new Command(async () =>
            {
                await DeleteUser();
            });

            PlayPlaylistCommand = new Command<GymPlaylistDTO>(async e => await StartThisPlayList(e));


        }
        public async Task StartThisPlayList(GymPlaylistDTO gymPlaylist)
        {
            Console.WriteLine(gymPlaylist);
            await Shell.Current.GoToAsync(nameof(Pages.ActiveWorkoutPage), new Dictionary<string, object>
            {
                ["PlayListId"] = gymPlaylist.Id
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
                await Task.Yield();
                await Shell.Current.Navigation.PopToRootAsync();
                return;
            }
            var playlists = await _gymPlaylistService.GetPlaylistDTOsByUserId(CurrentUser.Id);
            _allPlaylists = playlists;
            _currentPage = 0;
            ShowCurrentPage();
        }

        private void ChangePage(int direction)
        {
            int lastPage = Math.Max(0, (_allPlaylists.Count - 1) / PageSize);
            _currentPage = Math.Clamp(_currentPage + direction, 0, lastPage);
            ShowCurrentPage();
        }

        private void ShowCurrentPage()
        {
            CurrentThreePlaylists.Clear();
            foreach (var list in _allPlaylists.Skip(_currentPage * PageSize).Take(PageSize))
                CurrentThreePlaylists.Add(list);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void GreetUser()
        {
            ButtonText = " = )";
            GreetingUser = $"You are looking swole today {_loggedInUser.CurrentUser.Firstname} {_loggedInUser.CurrentUser.Id}!";
        }
        public async Task DeleteUser()
        {
            //finns ej id'n i objekt
            using var response = await _httpClient.DeleteAsync("api/User/" + _loggedInUser.CurrentUser.Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
