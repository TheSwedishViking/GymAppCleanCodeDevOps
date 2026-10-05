
using GymSwipe.ViewModels;
using System.Collections.ObjectModel;

namespace GymSwipe.Pages;

public partial class CardSwipe : ContentPage
{
	//Posiiton of card to manipulat
	private double _startX, _startY;

	private string _currentCard;
	private string _cardDeckInfo;
	public string CardDeckInfo
	{
		get { return _cardDeckInfo; }
		set
		{
			if(_cardDeckInfo==value) return;
			_cardDeckInfo = value;
            OnPropertyChanged(nameof(CardDeckInfo));
        }
    }

    //With the factor of the card width, this will set limit to register as discarded/approved
    public double CardPositionRegistrationThreshold { get; set; } = 1;
	public double SwipeLimitForRegistration => Card.Width * CardPositionRegistrationThreshold;
	private readonly CardSwipeViewModel _vm;
	public CardSwipe(CardSwipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
		Card.BackgroundColor = Colors.Black;
        _startX = Card.TranslationX; 
		_startY = Card.TranslationY;

    }

    public void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
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
				//UI
                Card.TranslationX = _startX + e.TotalX;
                Card.TranslationY = _startY + e.TotalY;
				Card.Rotation = (e.TotalX / _vm.CardDividend)*_vm.RotationFactor;
				if(Card.TranslationX <= _vm.DiscardX)
				{
                    Card.BackgroundColor = Colors.Red;
                }
				else if(Card.TranslationX >= _vm.ApproveX)
				{
                    Card.BackgroundColor = Colors.Green;
                }
				else
				{
                    Card.BackgroundColor = Colors.Black;
                }
                break;
			case GestureStatus.Completed:

				_vm.HandleOnCompleteCardSwipe(Card.TranslationX, Card.Width);
                ResetCard();

				break;
				//Cancel for resetting
			case GestureStatus.Canceled:
				Console.WriteLine("Cancelled");
                ResetCard();
                break;
		}

    }

    public void ResetCard()
	{
		Card.TranslationX = _startX;
		Card.TranslationY = _startY;
		Card.Rotation = 0;
		Card.BackgroundColor = Colors.Gray;
	}
}