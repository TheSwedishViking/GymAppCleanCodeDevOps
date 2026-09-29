using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.ExampleData;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseService : IExerciseService
    {
        public async Task<List<ExerciseDTO>> GetAllExercisesAsync()
        {
            List<ExerciseDTO> exercises = new List<ExerciseDTO>();
            foreach(var ex in StaticExerciseData.Exercises)
            {
                var dto = new ExerciseDTO
                {
                    Id = ex.Id,
                    Name = ex.Name,
                    TargetAreaId = ex.TargetAreaId,
                    TargetAreaName = "Test data",
                    VideoSourceLink = ex.VideoSourceLink
                };
                exercises.Add(dto); 
            }

            return exercises;
        }

        public Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

    }
}
