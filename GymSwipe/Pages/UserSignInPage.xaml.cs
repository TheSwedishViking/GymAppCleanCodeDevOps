using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserSignInPage : ContentPage
{
    public UserSignInPage(UserPageSignInViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;





    }
}