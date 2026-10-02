using GymSwipe.ViewModels.Admin;

namespace GymSwipe.Pages;

public partial class AdminRegisterNewExercise : ContentPage
{
	private readonly AdminRegisterNewExerciseViewModel _vm;
	public AdminRegisterNewExercise(AdminRegisterNewExerciseViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _vm.OnLoadPageGetExerciseAreas();
    }
}