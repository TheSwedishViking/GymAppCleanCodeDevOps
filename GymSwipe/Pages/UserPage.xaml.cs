using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserPage : ContentPage
{
    private readonly UserPageAccountViewModel _vm;
    public UserPage(UserPageAccountViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            await _vm.GetAllTheUsersPlaylists();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);   // so a failed API call isn't silent
        }
    }

    private async void OnClickGreetUser(object sender, EventArgs e)
    {
        lblGreeted.TextColor = Colors.Red;
    }
}