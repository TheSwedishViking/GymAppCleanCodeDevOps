using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        public GymUser CurrentUser => GymUser.CurrentUser;





        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterUserCommand { get; }
        public UserRegisterViewModel()
        {
            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
        }

        public async Task TryRegisterNewUser()
        {
            GymUser.CurrentUser.SetFriendCode();
            await Shell.Current.GoToAsync("..");
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
