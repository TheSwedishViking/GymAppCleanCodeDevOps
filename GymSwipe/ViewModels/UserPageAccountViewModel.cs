using GymSwipe.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {
        private GymUser _gymUser;
        public GymUser GymUser 
        {
            get { return _gymUser; }
            set
            {
                _gymUser = value;
                OnPropertyChanged(nameof(GymUser));
                _ = GreetUser();
            }
        
        }

        private string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }
        public ICommand GreetUserCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public UserPageAccountViewModel()
        {
            GreetUserCommand = new Command(async () => { 
                await GreetUser(); 
            });
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GreetUser()
        {
            Console.WriteLine("Hello!");
            ButtonText = "Heelo user! FUCCAK";

        }
    }
}
