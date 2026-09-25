using GymSwipe.Application.DTOs;
using GymSwipe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Services
{
    public class ExerciseService : IExerciseService
    {
        public Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
