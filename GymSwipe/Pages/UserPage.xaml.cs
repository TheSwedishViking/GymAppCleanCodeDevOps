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
            await _vm.InitalizeAsync();

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}