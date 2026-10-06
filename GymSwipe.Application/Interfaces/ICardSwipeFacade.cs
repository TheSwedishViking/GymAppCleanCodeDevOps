using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ICardSwipeFacade
    {
        bool HasDrawnAllCards();
        Task<string> ApproveCard(string card);
        Task<string> DiscardCard();
        Task<string> DrawNewCard();
        Task<string> DeckInfo();
        Task GetExercises();

    }
}
