using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Display;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using GymSwipe.UI_Helper;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageSignInViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public GymUser CurrentUser { get; set; } = new GymUser();
        public string EmailEntry { get; set; }
        public ICommand SignInCommand { get; }
        private LoggedInUser _loggedIn;


        private string _statusCheck = "";
        public string StatusCheck
        {
            get => _statusCheck;
            set
            {
                if (_statusCheck == value) return;
                _statusCheck = value;
                OnPropertyChanged(nameof(StatusCheck));
            }
        }

        private HttpClient _httpClient;
        private readonly IDisplayInfoHelper _displayInfoHelper;
        public UserPageSignInViewModel(HttpClient httpClient, LoggedInUser loggedInUser, IDisplayInfoHelper displayInfoHelper)
        {
            SignInCommand = new Command(async () => await TryLoginUser());
            _displayInfoHelper = displayInfoHelper;
            _httpClient = httpClient;
            _loggedIn = loggedInUser;
        }

        public async Task TryLoginUser()
        {

            bool existingEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/Unique-Email/" + EmailEntry);
            if (existingEmail)
            {
                StatusCheck = EmailEntry + " not found! Register a new one";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);

                return;
            }
            StatusCheck = EmailEntry + "Found User";

            var found = await _httpClient.GetFromJsonAsync<GymUserDTO>("api/User/Get-UserDto-By-Email/" + EmailEntry);
            if(found == null)
            {
                StatusCheck = "User not found";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            _loggedIn.CurrentUser = new GymUser //Setting the user to the singleton's gymUser
            {
                Id = found.Id,
                Firstname = found.Firstname,
                Surname = found.Surname,
                Email = found.Email,
                HeightCm = found.HeightCm,
                WeightKg = found.WeightKg,
                Gender = found.Gender,
            };
            _loggedIn.CurrentUser.SetFriendCode();
            await Shell.Current.GoToAsync("..");
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
