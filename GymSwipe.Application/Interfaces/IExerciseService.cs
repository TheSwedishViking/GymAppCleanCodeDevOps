using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDTO> GetExerciseByIdAsync(int id); 
    }
}
