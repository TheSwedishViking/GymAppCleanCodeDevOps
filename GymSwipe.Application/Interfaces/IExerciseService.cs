using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDTO> GetExerciseByIdAsync(int id);
        Task<List<ExerciseDTO>> GetAllExercisesAsync();
    }
}
