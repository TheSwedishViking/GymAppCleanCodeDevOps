using GymSwipe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IExerciseRepository
    {
        Task<List<ExerciseDTO>> GetExercisesByCategoryAsync(int categoryId);

    }
}
