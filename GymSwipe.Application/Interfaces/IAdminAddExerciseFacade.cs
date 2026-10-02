using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IAdminAddExerciseFacade
    {
        Task AddExercise(Exercise newExercise);
        Task<List<ExerciseTargetArea>> GetExerciseTargetsAsync();
    }
}
