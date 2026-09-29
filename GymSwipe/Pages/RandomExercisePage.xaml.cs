using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class RandomExercisePage : ContentPage
{
	public RandomExercisePage(RandomExerciseViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}