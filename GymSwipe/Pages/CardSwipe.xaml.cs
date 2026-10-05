namespace GymSwipe.Pages;

public partial class CardSwipe : ContentPage
{
	private double _startX, _startY;
	public CardSwipe()
	{
		InitializeComponent();
		_startX = CardShell.TranslationX; 
		_startY = CardShell.TranslationY;
	}

    public void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
		switch (e.StatusType)
		{
			//When starting to move, store position
			case GestureStatus.Started:
				_startX = CardShell.TranslationX;
				_startY = CardShell.TranslationY;
				break;
			//While moving
			case GestureStatus.Running:
                Console.WriteLine("WE moving?!");
                CardShell.TranslationX = _startX + e.TotalX;
                CardShell.TranslationY = _startY + e.TotalY;
				break;
		}
    }
}