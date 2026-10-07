using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageSignInViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand SignInCommand { get; }


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
        public UserPageSignInViewModel(HttpClient httpClient)
        {
            SignInCommand = new Command(async () => await TryRegisterNewUser());
            _httpClient = httpClient;
        }

        public async Task TryRegisterNewUser()
        {


            bool uniqueEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/email/" + correctEmail.Email);
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
