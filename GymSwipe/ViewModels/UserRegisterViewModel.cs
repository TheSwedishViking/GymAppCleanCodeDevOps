using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {

        private readonly UserInputValidatorService _validator = new();
        private readonly UserInputPropertiesValidator _validatorProps = new();
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
        private readonly IDisplayInfoHelper _displayInfoHelper;

        public UserRegisterViewModel(LoggedInUser loggedInUser, HttpClient httpClient, IDisplayInfoHelper displayInfoHelper)
        {
            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
            _loggedIn = loggedInUser;
            _httpClient = httpClient;
            _displayInfoHelper = displayInfoHelper;
        }

        public async Task TryRegisterNewUser()
        {

            var first = _validator.UserNameValidator(CurrentUser.Firstname);
            if (first is null)
            {
                StatusCheck = "Invalid first name. Use 2-20 letters.";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            CurrentUser.Firstname = first;
            var last = _validator.UserNameValidator(CurrentUser.Surname);
            if (last is null)
            {
                StatusCheck = "Invalid surname. Use 2-20 letters.";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            CurrentUser.Surname = last;

            if (CurrentUser.Email.IsNullOrEmpty())
            {
                StatusCheck = "Email cant be empty!";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            var correctEmail = _validator.UserEmailIsValid(CurrentUser.Email);

            if (correctEmail.IsValid == false)
            {
                StatusCheck = correctEmail.Email; //response message
                await _displayInfoHelper.DisplayStandardAlert("Email not valid");
                return;
            }
            bool uniqueEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/Unique-Email/" + correctEmail.Email);
            if (uniqueEmail == false)
            {
                StatusCheck = correctEmail.Email + " Email is taken!";
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            var validHeight = await _validatorProps.ValidateUserHeight(CurrentUser.HeightCm);
            if (validHeight.Success == false)
            {
                StatusCheck = validHeight.Message;
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
                return;
            }
            var validWeight = await _validatorProps.ValidateUserWeight(CurrentUser.WeightKg);
            if (validWeight.Success == false)
            {
                StatusCheck = validWeight.Message;
                await _displayInfoHelper.DisplayStandardAlert(StatusCheck);
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

            try
            {
                using var response = await _httpClient.PostAsJsonAsync("api/User", request);
                response.EnsureSuccessStatusCode();
                var user = await response.Content.ReadFromJsonAsync<GymUserDTO>(); //Set singleton id
                CurrentUser.Id = user.Id;
                _loggedIn.CurrentUser = CurrentUser;
                _loggedIn.CurrentUser.SetFriendCode();
            }
            catch (Exception ex)
            {
                await _displayInfoHelper.DisplayStandardAlert(ex.Message);
            }

            await Shell.Current.GoToAsync("..");
        }
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
