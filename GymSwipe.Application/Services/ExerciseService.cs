using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.ExampleData;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _repo;
        public ExerciseService(IExerciseRepository exerciseRepository)
        {
            _repo = exerciseRepository;
        }
        public async Task<List<ExerciseDTO>> GetAllExercisesAsync()
        {
            var exercises = await _repo.GetAllExercisesAsync();
            List<ExerciseDTO> dtos = exercises.Select(e =>

                new ExerciseDTO
                {
                    Id = e.Id,
                    VideoSourceLink = e.VideoSourceLink,
                    Name = e.Name,

                    TargetAreaNames = e.TargetAreas.
                    Select(e => e.ExerciseCategoryName).
                    ToList(),
                }).ToList();

            return dtos;
        }

        public async Task<List<ExerciseDTO>> GetExerciseByIdAsync(int id)
        {
            var chests = await _repo.GetExercisesByCategoryAsync(id);
            List<ExerciseDTO> dtos = chests.Select(e =>

              new ExerciseDTO
              {
                  Id = e.Id,
                  VideoSourceLink = e.VideoSourceLink,
                  Name = e.Name,

                  TargetAreaNames = e.TargetAreas.
                  Select(e => e.ExerciseCategoryName).
                  ToList(),
              }).ToList();

            return dtos;
        }

        public async Task<List<ExerciseDTO>?> GetRandomExercisesAsync()
        {
            var dtos = await GetAllExercisesAsync();
            var arr = dtos.ToArray();
            var randoms = arr.Shuffle().Take(3).ToList();
            return randoms;
        }
    }
}
