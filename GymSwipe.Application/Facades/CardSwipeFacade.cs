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

        private readonly IExerciseService _exerciseService;
        private readonly ITraningAreaService _traningAreaService;
        public CardSwipeFacade(IExerciseService exerciseService, ITraningAreaService traningAreaService)
        {
            _exerciseService = exerciseService;
            _traningAreaService = traningAreaService;
            AvailableCards = GetCards();
        }
        public bool HasDrawnAllCards() { return AvailableCards.Count() == 0; } 

        private List<string> GetCards()
        {
            return new List<string>
            {
                "boufallant_card.png",
                "metagross_card.jpg",
                "sceptile_card.jpg"
            };
        }

        public async Task<string> ApproveCard(string card)
        {
            AddedCards.Add(card);
           return await DrawNewCard();
        }

        public async Task<string> DiscardCard()
        {
          return await DrawNewCard();
        }

        public async Task<string> DrawNewCard()
        {
            if (HasDrawnAllCards())
            {
                return "";
            }

            var card = AvailableCards[Random.Shared.Next(AvailableCards.Count)];
            AvailableCards.Remove(card);
            return card;
        }

        public Task GetExercises()
        {
            throw new NotImplementedException();
        }

        public async Task<string> DeckInfo()
        {
            if (HasDrawnAllCards()){
                return "Last card!";
            }
            return $"There are {AvailableCards.Count} cards remaning in the deck";
        }
    }
}
