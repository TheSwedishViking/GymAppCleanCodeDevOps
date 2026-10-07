using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserSignInPage : ContentPage
{
    private readonly UserPageSignInViewModel _vm;
    public UserSignInPage(UserPageSignInViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
    }

}