using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        //private GymUser _gymUser = new();
        //public GymUser NewUser
        //{
        //    get { return _gymUser; }
        //    set
        //    {
        //        _gymUser = value;
        //        OnPropertyChanged(nameof(NewUser));
        //    }
        //}

        public GymUserDTO NewUser
        {
            set
            {
                GymUser.CurrentUser = value;
                OnPropertyChanged(nameof(GymUser.CurrentUser));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterUserCommand { get; }
        public UserRegisterViewModel()
        {
            RegisterUserCommand = new Command(async () => await TryRegisterNewUser());
        }

        public async Task TryRegisterNewUser()
        {
            Console.WriteLine(GymUser.CurrentUser);
            GymUser.CurrentUser.SetFriendCode();
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
