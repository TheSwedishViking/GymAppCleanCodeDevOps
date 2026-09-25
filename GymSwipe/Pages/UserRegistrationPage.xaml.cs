using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserRegistrationPage : ContentPage
{
	public UserRegistrationPage(UserRegisterViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}