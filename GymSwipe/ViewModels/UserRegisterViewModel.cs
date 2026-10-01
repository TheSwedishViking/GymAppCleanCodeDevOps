using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {



        public GymUser CurrentUser { get; set; } = new GymUser();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterUserCommand { get; }
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


            var request = new RequestCreateGymUserDTO
            {
                Firstname = CurrentUser.Firstname,
                Surname = CurrentUser.Surname,
                Email = CurrentUser.Email,
                HeightCm = CurrentUser.HeightCm,
                WeightKg = CurrentUser.WeightKg,
                Gender = CurrentUser.Gender
            };

            using var response = await _httpClient.PostAsJsonAsync("api/User", request);
            response.EnsureSuccessStatusCode();





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
