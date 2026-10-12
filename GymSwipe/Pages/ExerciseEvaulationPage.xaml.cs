using GymSwipe.ViewModels;

namespace GymSwipe.Pages;

public partial class ExerciseEvaulationPage : ContentPage
{
	private readonly ExerciseEvaulationViewModel _vm;
	public ExerciseEvaulationPage(ExerciseEvaulationViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _vm.InitalizeAsync();
    }
}