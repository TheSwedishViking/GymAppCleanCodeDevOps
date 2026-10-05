using GymSwipe.ViewModels.Admin;

namespace GymSwipe.Pages;

public partial class AdminAddNewTargetArea : ContentPage
{
	private readonly AdminRegisterNewTrainingAreaViewModel _vm;
	public AdminAddNewTargetArea(AdminRegisterNewTrainingAreaViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
	}
    protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _vm.GetAreas();
	}
}