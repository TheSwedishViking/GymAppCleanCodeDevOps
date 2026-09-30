using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseFacade
    {
        Task<List<ExerciseDTO>> GetAllExercisesAsync();
        Task<List<ExerciseDTO>> GetRandomExercisesAsync();
        Task<ExerciseDTO> GetExerciseAsyncById(int id);
        Task<List<ExerciseDTO>> GetExerciseListByCategoryAsync(int id);
        Task<List<ExerciseDTO>> GetChestExerciesAsync(int id);
    }
}
