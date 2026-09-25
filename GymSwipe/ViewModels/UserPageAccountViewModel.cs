using GymSwipe.Domain.Models;
using GymSwipe.ExampleData;
using System.ComponentModel;
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
        private UserClass _Lbllabel;
        public UserClass LblText
        {
            get { return _Lbllabel; }
            set
            {
                _Lbllabel = value;
                OnPropertyChanged(nameof(LblText));
            }
        }


        private string _buttonText = "Login";
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
            GreetUserCommand = new Command(async () =>
            {
                await GreetUser();
            });
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GreetUser()
        {

            var user = userLoginTest.ExampleData();
            ButtonText = "Logout";
            LblText = user;
        }
    }
}
