using GymSwipe.Domain.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class UserPageAccountViewModel : INotifyPropertyChanged
    {

        public GymUser CurrentUser => GymUser.CurrentUser;




        private string _buttonText = "Todays Message!";
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
            ButtonText = " = )";
        }
    }
}
