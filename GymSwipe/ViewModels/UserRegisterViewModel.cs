using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces.Display;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.ApplicationLayer.Services.Validators;
using GymSwipe.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        public RequestCreateGymUserDTO CurrentUser { get; set; } = new RequestCreateGymUserDTO();

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
        private readonly IUserRegisterFacade _userRegister;
        private readonly IDisplayInfoHelper _displayInfoHelper;
        private readonly IMapDomainService _mapDomainService;

        public UserRegisterViewModel(LoggedInUser loggedInUser, HttpClient httpClient, IDisplayInfoHelper displayInfoHelper, IUserRegisterFacade userRegisterFacade, IMapDomainService mapDomainService)
        {

            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
            _loggedIn = loggedInUser;
            _httpClient = httpClient;
            _userRegister = userRegisterFacade;
            _displayInfoHelper = displayInfoHelper;
            _mapDomainService = mapDomainService;
        }

        public async Task TryRegisterNewUser()
        {
            List<string> errors = await _userRegister.ValidateUser(CurrentUser);
            if (errors.Count > 0)
            {
                await _displayInfoHelper.DisplayErrorAlert(errors);
                return;
            }

            List<string> emailErrors = await _userRegister.CheckIfEmailIsUnique(CurrentUser.Email);
            if(emailErrors.Count > 0)
            {
                await _displayInfoHelper.DisplayErrorAlert(emailErrors);
                return;
            }
            CurrentUser.Email = await _userRegister.GetCorrectFormatedEmail(CurrentUser.Email);
            //Everythings okay till we get down here, should've returned by now
            try
            {
                var user = await _userRegister.CreateUserFromRegistration(CurrentUser);
                var domain = await _mapDomainService.MapGymUserDomain(user);
           
                _loggedIn.CurrentUser = domain;
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
