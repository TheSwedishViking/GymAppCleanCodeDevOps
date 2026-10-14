using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces.Repositories
{
    public interface ITraningAreaRepo
    {
        Task<List<ExerciseTargetArea>> GetExerciseTargetAreasAsync();
        Task<ExerciseTargetArea> GetExerciseTargetAreaByName(string name);
        Task<bool> AddTargetArea(ExerciseTargetArea area);
        Task RemoveTargetArea(int id);
        Task<string> GetTargetAreaImage(int id);
        Task<ExerciseTargetArea> GetAreaById(int id);
    }
}
