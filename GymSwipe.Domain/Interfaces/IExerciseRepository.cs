using GymSwipe.Domain.Models;

namespace GymSwipe.Domain.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<Exercise>> GetExercisesByCategoryAsync(int categoryId);

    }
}
