using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserPage : ContentPage
{
    public UserPage(UserPageAccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnClickGetUser(object sender, EventArgs e)
    {

    }
}