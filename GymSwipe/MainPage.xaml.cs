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
        private async void RandomExerciseButtonButtonClicked(object? sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.RandomExercisePage));
        }

        private async void Admin_NewExerciseButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.AdminRegisterNewExercise));
        }

        private async void Admin_NewTargetAreaButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(Pages.AdminAddNewTargetArea));
        }
    }
}
