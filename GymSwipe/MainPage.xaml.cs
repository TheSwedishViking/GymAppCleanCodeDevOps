using GymSwipe.Domain.Models;
using GymSwipe.ViewModels;

namespace GymSwipe
{
    public partial class MainPage : ContentPage
    {

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void onUserPageBtnClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.UserPage));
        }

        private async void RegisterUserNavigationButtonClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.UserRegistrationPage));

        }


    }
}
