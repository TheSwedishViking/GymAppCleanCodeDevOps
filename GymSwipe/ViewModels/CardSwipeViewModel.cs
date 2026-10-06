using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ViewModels
{
    public class CardSwipeViewModel : INotifyPropertyChanged
    {
        public List<string> Cards { get; set; } = new List<string>
        {
            "boufallant_card.png",
            "metagross_card.jpg",
            "sceptile_card.jpg"
        };
        public ObservableCollection<string> AddedCards { get; } = new ObservableCollection<string>();
        public bool DrawnAllCards => Cards.Count == 0;
        public int CardsRemaning => Cards.Count();
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
        public CardSwipeViewModel()
        {
            DiscardX -= SwipeLimitForRegistration;
            ApproveX += SwipeLimitForRegistration;
            DrawNextCard();
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     
        public void HandleOnCompleteCardSwipe(double translationX, double width)
        {
            //From center to left => Negative values?s
            if (translationX <= -SwipeLimitForRegistration)
            {
                Console.WriteLine("Discarded");
                DiscardCard();
            }

            else if (translationX >= SwipeLimitForRegistration)
            {
                Console.WriteLine("Approved");
                ApproveCard();
            }
        }
        public void ApproveCard()
        {
            if (_currentCard != null)
            {
                AddedCards.Add(_currentCard);
                DrawNextCard();
            }

        }
        public void DiscardCard()
        {
            if (_currentCard != null)
            {
                DrawNextCard();
            }
        }
        public void DrawNextCard()
        {
            if (DrawnAllCards)
            {
                _currentCard = null;
                CardDeckInfo = "All cards have been drawn!";
                CardImageSource = null;
                return;
            }

            var card = Cards[Random.Shared.Next(Cards.Count)];
            Cards.Remove(card);

            _currentCard = card;
            CardImageSource = card;
            CardDeckInfo = $"There are {CardsRemaning + 1} cards remaning in the deck";

        }

    }
}
