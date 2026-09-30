using GymSwipe.ApplicationLayer.Services;
using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        public GymUser CurrentUser { get; set; } = new GymUser();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterUserCommand { get; }
        private LoggedInUser _loggedIn;
        public UserRegisterViewModel(LoggedInUser loggedInUser)
        {
            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
            _loggedIn = loggedInUser;
        }

        public async Task TryRegisterNewUser()
        {
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
