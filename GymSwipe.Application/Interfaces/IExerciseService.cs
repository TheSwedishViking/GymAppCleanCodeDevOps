using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Enums;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseDTO>> GetExerciseByIdAsync(int id);
        Task<ExerciseDTO> GetExerciseByName(string name);
        Task<List<ExerciseDTO>> GetAllExercisesAsync();
        Task<List<ExerciseDTO>?> GetRandomExercisesAsync();
        Task<List<ExerciseDTO>?> GetExerciseByEnum(AreaEnum area);
        Task<bool> SaveExercise(ExerciseDTO newExercise);
    }
}
