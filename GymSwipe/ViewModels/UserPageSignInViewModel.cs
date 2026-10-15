using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Display;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
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
        private IMapDomainService _mapDomainService;
        private LoggedInUser _loggedIn;

        private readonly IUserRegisterFacade _userRegister;
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
        public UserPageSignInViewModel(HttpClient httpClient, 
            LoggedInUser loggedInUser,
            IDisplayInfoHelper displayInfoHelper,
            IMapDomainService mapDomainService,
            IUserRegisterFacade userRegisterFacade
            )
        {
            SignInCommand = new Command(async () => await TryLoginUser());
            _mapDomainService = mapDomainService;
            _displayInfoHelper = displayInfoHelper;
            _userRegister = userRegisterFacade;
            _httpClient = httpClient;
            _loggedIn = loggedInUser;
        }

        public async Task TryLoginUser()
        {
            List<string> errors = await _userRegister.ValidateEmail(EmailEntry);

            if(errors.Count > 0)
            {
                await _displayInfoHelper.DisplayErrorAlert(errors);
                return;
            }
            var correctFormated = await _userRegister.GetCorrectFormatedEmail(EmailEntry);
            var found =await _userRegister.GetUserByEmail(correctFormated);

            if(found ==  null)
            {
                await _displayInfoHelper.DisplayStandardAlert("User not found with email");
            }
            var domain = await _mapDomainService.MapGymUserDomain(found);
            _loggedIn.CurrentUser = domain; //Setting the user to the singleton's gymUser
        
            await Shell.Current.GoToAsync("..");
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
