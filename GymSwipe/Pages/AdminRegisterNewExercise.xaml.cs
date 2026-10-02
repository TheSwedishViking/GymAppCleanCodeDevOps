using GymSwipe.ViewModels.Admin;

namespace GymSwipe.Pages;

public partial class AdminRegisterNewExercise : ContentPage
{
	public AdminRegisterNewExercise(AdminRegisterNewExerciseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}