using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class ActiveWorkoutPage : ContentPage
{
	private ActiveWorkoutViewModel _vm;
	public ActiveWorkoutPage(ActiveWorkoutViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
}