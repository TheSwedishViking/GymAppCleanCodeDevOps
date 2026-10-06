using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {

        public GymUser CurrentUser { get; set; }
        public ObservableCollection<GymPlaylist> UserPlaylists { get; } = new();



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
        public ICommand GreetUserCommand { get; }
        public ICommand DeleteUserCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private LoggedInUser _loggedInUser;
        private HttpClient _httpClient;
        private GymSwipe.ApplicationLayer.Interfaces.IGymPlaylistService _gymPlaylistService;
        public UserPageAccountViewModel(LoggedInUser loggedInUser, HttpClient httpClient, IGymPlaylistService gymPlaylistService)
        {
            _loggedInUser = loggedInUser;
            _httpClient = httpClient;
            CurrentUser = _loggedInUser.CurrentUser;
            _gymPlaylistService = gymPlaylistService;
            GreetUserCommand = new Command(async () =>
            {
                await GreetUser();
            });
            DeleteUserCommand = new Command(async () =>
            {
                await DeleteUser();
            });
        }

        public async Task GetAllTheUsersPlaylists()
        {
            var playLists = await _gymPlaylistService.GetPlaylistsByUserId(CurrentUser.Id);
            UserPlaylists.Clear();
            foreach (var list in playLists)
            {
                UserPlaylists.Add(list);
            }
        }



        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GreetUser()
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
