using GymSwipe.ApplicationLayer.DTOs;
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
        public ObservableCollection<ExerciseDTO> AddedExercises { get; } = new ObservableCollection<ExerciseDTO>();
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
        private bool _isDeckFinished;
        public bool IsDeckFinished
        {
            get => _isDeckFinished;
            private set
            {
                if (_isDeckFinished == value) return;
                _isDeckFinished = value;
                OnPropertyChanged(nameof(IsDeckFinished));
                OnPropertyChanged(nameof(IsCardStructureVisible));

            }
        }
        public bool IsCardStructureVisible => !IsDeckFinished;

        //With the factor of the card width, this will set limit to register as discarded/approved
        public double CardPositionRegistrationThreshold { get; set; } = 1;
        public double CardWidth { get; } = 300;
        public double DiscardX { get;  }
        public double ApproveX { get;  }
        public double SwipeLimitForRegistration => CardWidth * CardPositionRegistrationThreshold;
        public double CardDividend { get; } = 100;
        public double RotationFactor { get;  } = 5.02;
        private ExerciseDTO _currentExercise;
        public ExerciseDTO CurrentExercise
        {
            get => _currentExercise;
            set
            {
                if(_currentExercise == value) return;
                _currentExercise = value;
                OnPropertyChanged(nameof(CurrentExercise));
            }
        }
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
        }
        public async Task InitalizeAsync()
        {
            await _cardFacade.InitalizeAsync();
            await ShowInitalCard();
        }
        public async Task<bool> AllCardsAreDrawn()
        {
            return  _cardFacade.HasDrawnAllCards() && CurrentExercise==null;
        }

        private async Task ShowInitalCard()
        {
            _currentCard = await _cardFacade.DrawNewCard();
            CurrentExercise = await _cardFacade.DrawNewExerciseCard();
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
            if (CurrentExercise != null)
            {
                CurrentExercise = await _cardFacade.ApproveCard(_currentExercise);
                AddedExercises.Add(CurrentExercise);
                //AddedCards.Add(_currentCard);
                await ValidateAfterSwipe();
            }

        }
        public async Task DiscardCard()
        {
            if (CurrentExercise != null)
            {
                CurrentExercise = await _cardFacade.DiscardCard();
                await ValidateAfterSwipe();
            }

        }
        private async Task ValidateAfterSwipe()
        {
            if(CurrentExercise is null)
            {
                IsDeckFinished = true;
                CardImageSource = null;
                CardDeckInfo = "All cards have been drawn! Leave!";
                return;
            }
            await UpdateCard();
        }
        public async Task UpdateCard()
        {

            CardImageSource = await _cardFacade.GetApproptiateImageForExercise(CurrentExercise);
            CardDeckInfo = await _cardFacade.DeckInfo();
        }

    
    }
}
