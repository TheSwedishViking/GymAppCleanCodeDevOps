using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseService : IExerciseService
    {
        public Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
