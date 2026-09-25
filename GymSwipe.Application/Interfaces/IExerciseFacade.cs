using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseFacade
    {
        Task<ExerciseDTO> GetExerciseAsyncById(int id);
        Task<List<ExerciseDTO>> GetExerciseListByCategoryAsync(int id);
    }
}
