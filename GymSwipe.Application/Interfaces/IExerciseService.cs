using GymSwipe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IExerciseService
    {
        Task<ExerciseDTO> GetExerciseByIdAsync(int id); 
    }
}
