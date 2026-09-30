using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<IExerciseRepository>> GetExercisesByCategoryAsync(int categoryId);
        Task<List<Exercise>> GetAllExercisesAsync();

    }
}
