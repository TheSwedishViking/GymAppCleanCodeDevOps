using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITraningAreaRepo
    {
        Task<List<ExerciseTargetArea>> GetExerciseTargetAreasAsync();
        Task<ExerciseTargetArea> GetExerciseTargetAreaByName(string name);
        Task<bool> AddTargetArea(ExerciseTargetArea area);
        Task RemoveTargetArea(int id);
        Task UpdateTargetArea(ExerciseTargetArea area);
        Task<string> GetTargetAreaImage(int id);

    }
}
