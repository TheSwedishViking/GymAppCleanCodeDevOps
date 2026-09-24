using GymSwipe.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserRegisterViewModel : INotifyPropertyChanged
    {
        private GymUser _gymUser = new();
        public GymUser NewUser
        {
            get { return _gymUser; }
            set 
            {
                _gymUser = value;
                OnPropertyChanged(nameof(NewUser));
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
            Console.WriteLine(NewUser);
            NewUser.SetFriendCode();
        }

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
