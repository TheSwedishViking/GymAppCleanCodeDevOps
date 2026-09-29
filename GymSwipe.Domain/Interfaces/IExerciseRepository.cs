using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetExercisesByCategoryAsync(int categoryId);

    }
}
