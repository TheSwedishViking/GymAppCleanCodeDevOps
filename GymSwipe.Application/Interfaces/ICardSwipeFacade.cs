using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ICardSwipeFacade
    {
        Task ApproveCard();
        Task DiscardCard();
        Task DrawNewCard();
        Task GetExercises();

    }
}
