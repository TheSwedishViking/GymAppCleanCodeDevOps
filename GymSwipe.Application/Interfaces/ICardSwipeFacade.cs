using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ICardSwipeFacade
    {
        bool HasDrawnAllCards();
        Task<ExerciseDTO> DrawNewExerciseCard();
        Task InitalizeAsync();
        Task<ExerciseDTO> ApproveCard(ExerciseDTO card);
        Task<ExerciseDTO> DiscardCard();
        Task<string> DrawNewCard();
        Task<string> DeckInfo();
        Task GetExercises();
        Task<string> GetApproptiateImageForExercise(ExerciseDTO currentExercise);
    }
}
