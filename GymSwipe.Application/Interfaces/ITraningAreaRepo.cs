using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITraningAreaRepo
    {
        Task<List<ExerciseTargetArea>> GetExerciseTargetAreasAsync();
        Task<ExerciseTargetArea> GetExerciseTargetAreaByName(string name);
        Task AddTargetArea(ExerciseTargetArea area);
        Task RemoveTargetArea(int id);
        Task UpdateTargetArea(ExerciseTargetArea area);

    }
}
