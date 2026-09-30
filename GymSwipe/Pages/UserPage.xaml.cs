using GymSwipe.Domain.Models;
using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserPage : ContentPage
{
    public UserPage(UserPageAccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnClickGreetUser(object sender, EventArgs e)
    {
        lblGreeted.TextColor = Colors.Red;
    }
}