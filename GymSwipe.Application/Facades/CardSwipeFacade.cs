using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class CardSwipeFacade : ICardSwipeFacade
    {
        public List<string> AvailableCards { get; set; } = new List<string>();
        public List<string> AddedCards { get; set; } = new List<string>();
        public List<ExerciseDTO> AvailableExercises { get; set; } = new List<ExerciseDTO>();
        public List<ExerciseDTO> AddedExercises { get; set; } = new List<ExerciseDTO>();

        private readonly IExerciseService _exerciseService;
        private readonly ITraningAreaService _traningAreaService;
        public CardSwipeFacade(IExerciseService exerciseService, ITraningAreaService traningAreaService)
        {
            _exerciseService = exerciseService;
            _traningAreaService = traningAreaService;
            AvailableCards = GetCards();
        }
        public async Task InitalizeAsync()
        {
            await GetExercises();
        }
        public async Task GetExercises()
        {
            AvailableExercises = await _exerciseService.GetAllExercisesAsync();
        }
        public bool HasDrawnAllCards() { return AvailableExercises.Count() == 0; } 

        private List<string> GetCards()
        {
            return new List<string>
            {
                "boufallant_card.png",
                "metagross_card.jpg",
                "sceptile_card.jpg"
            };
        }
        public async Task<ExerciseDTO> ApproveCard(ExerciseDTO exercise)
        {
            AddedExercises.Add(exercise);
            return await DrawNewExerciseCard();
        }
        public async Task<ExerciseDTO> DiscardCard()
        {
            return await DrawNewExerciseCard();
        }

        public async Task<string> DrawNewCard()
        {
            if (HasDrawnAllCards())
            {
                return "";
            }
            var card = AvailableCards[Random.Shared.Next(AvailableCards.Count)];
            //AvailableCards.Remove(card);
            return card;
        }
        public async Task<ExerciseDTO> DrawNewExerciseCard()
        {
            if (HasDrawnAllCards())
            {
                return null;
            }
            var exercise = AvailableExercises[Random.Shared.Next(AvailableExercises.Count)];
            AvailableExercises.Remove(exercise);
            return exercise;
        }

        public async Task<string> DeckInfo()
        {
            if (HasDrawnAllCards()){
                return "Last card!";
            }
            return $"There are {AvailableExercises.Count} cards remaning in the deck";
        }

        public async Task<string> GetApproptiateImageForExercise(ExerciseDTO currentExercise)
        {
        
            return AvailableCards[Random.Shared.Next(AvailableCards.Count)];
        }
    }
}
