using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
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
        Task LoadExercisesById(int? exerciseTypeId);
        Task<List<ExerciseDTO>> GetLoadedExercises();
        Task<GymUser> GetCurrentUser();
        Task SavePlayList(GymUser curentUser);
        Task<string> GetApproptiateImageForExercise(ExerciseDTO currentExercise);
    }
}
