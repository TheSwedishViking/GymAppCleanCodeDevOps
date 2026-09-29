using GymSwipe.Domain.Models;
using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserPage : ContentPage
{
    public UserPage()
    {
        InitializeComponent();
        BindingContext = new UserPageAccountViewModel();
    }

    private async void OnClickGreetUser(object sender, EventArgs e)
    {
        lblGreeted.Text = "You are looking swole today " + GymUser.CurrentUser.Firstname + "!";
        lblGreeted.TextColor = Colors.Red;
    }
}