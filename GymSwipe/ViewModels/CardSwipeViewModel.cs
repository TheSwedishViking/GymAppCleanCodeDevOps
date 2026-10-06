using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ViewModels
{
    public class CardSwipeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> AddedCards { get; } = new ObservableCollection<string>();
        private string _currentCard;
        private string _cardDeckInfo;
        public string CardDeckInfo
        {
            get { return _cardDeckInfo; }
            set
            {
                if (_cardDeckInfo == value) return;
                _cardDeckInfo = value;
                OnPropertyChanged(nameof(CardDeckInfo));
            }
        }

        //With the factor of the card width, this will set limit to register as discarded/approved
        public double CardPositionRegistrationThreshold { get; set; } = 1;
        public double CardWidth { get; } = 300;
        public double DiscardX { get;  }
        public double ApproveX { get;  }
        public double SwipeLimitForRegistration => CardWidth * CardPositionRegistrationThreshold;
        public double CardDividend { get; } = 100;
        public double RotationFactor { get;  } = 5.02;
        private string _cardImageSource;
        public string CardImageSource
        {
            get => _cardImageSource;
            set
            {
                if(_cardImageSource == value) return;
                _cardImageSource = value;
                OnPropertyChanged(nameof(CardImageSource));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly ICardSwipeFacade _cardFacade;
        public CardSwipeViewModel(ICardSwipeFacade cardSwipeFacade)
        {
            DiscardX -= SwipeLimitForRegistration;
            ApproveX += SwipeLimitForRegistration;
            _cardFacade = cardSwipeFacade;
            _ = ShowInitalCard();
          
            //DrawNextCard();
        }
        private async Task ShowInitalCard()
        {
            _currentCard = await _cardFacade.DrawNewCard();
            await UpdateCard();
        }
    
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     
        public async Task HandleOnCompleteCardSwipe(double translationX, double width)
        {
            //From center to left => Negative values?s
            if (translationX <= -SwipeLimitForRegistration)
            {
                Console.WriteLine("Discarded");
                await DiscardCard();
            }

            else if (translationX >= SwipeLimitForRegistration)
            {
                Console.WriteLine("Approved");
                await ApproveCard();
            }
        }
        public async Task ApproveCard()
        {
            if (_currentCard != null)
            {
                _currentCard = await _cardFacade.ApproveCard(_currentCard);
                AddedCards.Add(_currentCard);
                await UpdateCard();
            }

        }
        public async Task DiscardCard()
        {
            if (_currentCard != null)
            {
                _currentCard = await _cardFacade.DiscardCard();
                await UpdateCard();
            }
        }
        public async Task UpdateCard()
        {
            CardImageSource = _currentCard;

            CardDeckInfo = await _cardFacade.DeckInfo();

        }

    }
}
