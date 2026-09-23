using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class UserPage : ContentPage
{
	public UserPage(UserPageAccountViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}