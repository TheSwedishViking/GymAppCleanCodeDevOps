namespace GymSwipe.Pages;

public partial class CardSwipe : ContentPage
{
	//Posiiton of card to manipulat
	private double _startX, _startY;
	public List<string> Cards { get; set; } = new List<string>
	{
		"boufallant_card.png",
		"metagross_card.jpg",
		"sceptile_card.jpg"
	};

	//With the factor of the card width, this will set limit to register as discarded/approved
	public double CardPositionRegistrationThreshold { get; set; } = 1;
	public double SwipeLimitForRegistration => Card.Width * CardPositionRegistrationThreshold;
	public CardSwipe()
	{
		InitializeComponent();
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
                Console.WriteLine("WE moving?!");
                Card.TranslationX = _startX + e.TotalX;
                Card.TranslationY = _startY + e.TotalY;
				Card.Rotation = (e.TotalX / 100) * 5.02;
				//Visuals
				if(Card.TranslationX <= -SwipeLimitForRegistration)
				{
                    Card.BackgroundColor = Colors.Red;
                }
				else if(Card.TranslationX >= SwipeLimitForRegistration)
				{
                    Card.BackgroundColor = Colors.Green;
                }
				else
				{
                    Card.BackgroundColor = Colors.Black;

                }

                break;
			case GestureStatus.Completed:
				Console.WriteLine("Gesture completed");
				//Validate if discard or approved
				double swipeLimit = Card.Width * CardPositionRegistrationThreshold;

				//From center to left => Negative values?s
				if(Card.TranslationX<= -swipeLimit)
				{
                    Console.WriteLine("Discarded");
				}

				else if(Card.TranslationX >= swipeLimit)
				{
                    Console.WriteLine("Approved");
					ApproveCard();
				}

				ResetCard();

				break;
				//Cancel for resetting
			case GestureStatus.Canceled:
				Console.WriteLine("Cancelled");
                ResetCard();
                break;
		}

    }
	public void ApproveCard()
	{
		CardImage.Source = Cards[Random.Shared.Next(0, Cards.Count())];
	}
	public void ResetCard()
	{
		Card.TranslationX = _startX;
		Card.TranslationY = _startY;
		Card.Rotation = 0;
		Card.BackgroundColor = Colors.Gray;
	}
}