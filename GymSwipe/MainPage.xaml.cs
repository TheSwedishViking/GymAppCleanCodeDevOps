using GymSwipe.Domain.Models;

namespace GymSwipe
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = GymUser.CurrentUser;
        }



        private async void onUserPageBtnClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.UserPage));
        }

        private async void RegisterUserNavigationButtonClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.UserRegistrationPage));

            Console.WriteLine(GymUser.CurrentUser);
        }


    }
}
