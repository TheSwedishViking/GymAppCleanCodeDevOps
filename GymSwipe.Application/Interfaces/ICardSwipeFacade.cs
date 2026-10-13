using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ICardSwipeFacade
    {
        bool HasDrawnAllCards();
        Task<ExerciseDTO> DrawNewExerciseCard();
        Task LoadExercisesAsync();
        Task<ExerciseDTO> ApproveCard(ExerciseDTO card);
        Task<ExerciseDTO> DiscardCard();
        Task<string> DeckInfo();
        Task LoadExercisesById(int? exerciseTypeId);
        Task SavePlayList();
        Task<string> GetApproptiateImageForExercise(int id);
        Task<List<ExerciseDTO>> GetAddedExercises();
        Task<List<ExerciseDTO>> GetLoadedExercises();
    }
}
