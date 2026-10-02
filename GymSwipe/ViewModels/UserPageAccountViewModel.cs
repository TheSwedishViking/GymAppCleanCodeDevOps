using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {

        public GymUser CurrentUser { get; set; }

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

        public UserPageAccountViewModel(LoggedInUser loggedInUser, HttpClient httpClient)
        {
            _loggedInUser = loggedInUser;
            _httpClient = httpClient;
            CurrentUser = _loggedInUser.CurrentUser;

            GreetUserCommand = new Command(async () =>
            {
                await GreetUser();
            });
            DeleteUserCommand = new Command(async () =>
            {
                await DeleteUser();
            });
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
