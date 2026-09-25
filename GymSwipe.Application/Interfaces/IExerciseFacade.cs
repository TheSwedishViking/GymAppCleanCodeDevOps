using GymSwipe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IExerciseFacade
    {
        Task<ExerciseDTO> GetExerciseAsyncById(int id);
        Task<List<ExerciseDTO>> GetExerciseListByCategoryAsync(int id);
    }
}
