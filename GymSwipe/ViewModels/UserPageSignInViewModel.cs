using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageSignInViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public GymUser CurrentUser { get; set; } = new GymUser();
        public string EmailEntry;
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


            bool uniqueEmail = await _httpClient.GetFromJsonAsync<bool>("api/User/email/" + EmailEntry);
            if (uniqueEmail = false)
            {
                StatusCheck = EmailEntry + " not found! Register a new one";
                return;
            }





            await Shell.Current.GoToAsync("..");
        }





        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
