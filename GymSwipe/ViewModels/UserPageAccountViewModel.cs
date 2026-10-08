using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using GymSwipe.ApplicationLayer.Interfaces;


namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {

        private List<GymPlaylist> _allPlaylists = new();
        private int _currentPage = 0;
        private const int PageSize = 3;
        public ObservableCollection<GymPlaylist> CurrentThreePlaylists { get; } = new();


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



        public event PropertyChangedEventHandler? PropertyChanged;

    
        public GymUser CurrentUser { get; private set; }
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
            var playlists = await _gymPlaylistService.GetPlaylistsByUserId(CurrentUser.Id);
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
