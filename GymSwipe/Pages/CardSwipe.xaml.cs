
using GymSwipe.ViewModels;
using System.Collections.ObjectModel;

namespace GymSwipe.Pages;

public partial class CardSwipe : ContentPage
{
	//Posiiton of card to manipulat
	private double _startX, _startY;
	private readonly CardSwipeViewModel _vm;
	public CardSwipe(CardSwipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
		//Inital base value for a card on the page
		Card.BackgroundColor = Colors.Black;
        _startX = Card.TranslationX; 
		_startY = Card.TranslationY;
    }
    protected async override void OnAppearing()
	{
		//Retrives exercises async
		base.OnAppearing();
        await _vm.InitalizeAsync();
    }
	public async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
	{
		//If there's cards left
		if (!_vm.IsDeckFinished)
		{
			switch (e.StatusType)
			{
				//When starting to move, store position
				case GestureStatus.Started:
					_startX = Card.TranslationX;
					_startY = Card.TranslationY;
					break;
				//While moving
				case GestureStatus.Running:
					//UI rotation and movement
					Card.TranslationX = _startX + e.TotalX;
					Card.TranslationY = _startY + e.TotalY;
					Card.Rotation = (e.TotalX / _vm.CardDividend) * _vm.RotationFactor;

					//Basic red for discard, green for approve
					if (Card.TranslationX <= _vm.DiscardX)
					{
						Card.BackgroundColor = Colors.Red;
					}
					else if (Card.TranslationX >= _vm.ApproveX)
					{
						Card.BackgroundColor = Colors.Green;
					}
					else
					{
						Card.BackgroundColor = Colors.Black;
					}
					break;
				case GestureStatus.Completed:
					//Pass result to the viewmodel, passes along to facade
					await _vm.HandleOnCompleteCardSwipe(Card.TranslationX, Card.Width);
					ResetCard();
					break;
				//Cancel for resetting
				case GestureStatus.Canceled:
					Console.WriteLine("Cancelled");
					ResetCard();
					break;
			}

		}
	}
	//Purely UI reset
    public void ResetCard()
	{
		Card.TranslationX = _startX;
		Card.TranslationY = _startY;
		Card.Rotation = 0;
		Card.BackgroundColor = Colors.Gray;
	}
}