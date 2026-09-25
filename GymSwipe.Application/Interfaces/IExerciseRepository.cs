using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseDTO>> GetExercisesByCategoryAsync(int categoryId);

    }
}
