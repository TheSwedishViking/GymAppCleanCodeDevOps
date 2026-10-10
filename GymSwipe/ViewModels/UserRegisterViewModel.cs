using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {

        private readonly UserInputValidatorService _validator = new();


        public GymUser CurrentUser { get; set; } = new GymUser();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterUserCommand { get; }

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

        private LoggedInUser _loggedIn;
        private HttpClient _httpClient;
        public UserRegisterViewModel(LoggedInUser loggedInUser, HttpClient httpClient)
        {
            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
            _loggedIn = loggedInUser;
            _httpClient = httpClient;
        }

        public async Task TryRegisterNewUser()
        {

            var first = _validator.UserNameValidator(CurrentUser.Firstname);
            if (first is null)
            {
                StatusCheck = "Invalid first name. Use 3-15 letters.";
                return;
            }
            CurrentUser.Firstname = first;
            var last = _validator.UserNameValidator(CurrentUser.Surname);
            if (last is null)
            {
                StatusCheck = "Invalid surname. Use 3-15 letters.";
                return;
            }
            CurrentUser.Surname = last;

            var correctEmail = _validator.UserEmailIsValid(CurrentUser.Email);

            if (correctEmail.IsValid == false)
            {
                StatusCheck = "Email incorrect format. Good luck";

                return;
            }
            bool uniqueEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/Unique-Email/" + correctEmail.Email);
            if (uniqueEmail == false)
            {
                StatusCheck = correctEmail.Email + " already in use. Try being original!";
                return;
            }

            var request = new RequestCreateGymUserDTO
            {
                Firstname = CurrentUser.Firstname,
                Surname = CurrentUser.Surname,
                Email = correctEmail.Email,
                HeightCm = CurrentUser.HeightCm,
                WeightKg = CurrentUser.WeightKg,
                Gender = CurrentUser.Gender
            };

            using var response = await _httpClient.PostAsJsonAsync("api/User", request);
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<GymUserDTO>();
            Console.WriteLine(user);


            CurrentUser.Id = user.Id;

            _loggedIn.CurrentUser = CurrentUser;
            Console.WriteLine(CurrentUser);
            _loggedIn.CurrentUser.SetFriendCode();
            await Shell.Current.GoToAsync("..");
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
