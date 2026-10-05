using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Enums;
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
        private readonly ITraningAreaRepo _traningAreaRepo;
        public ExerciseService(IExerciseRepository exerciseRepository, ITraningAreaRepo traningAreaRepo)
        {
            _repo = exerciseRepository;
            _traningAreaRepo = traningAreaRepo;
        }
        public async Task<List<ExerciseDTO>> GetAllExercisesAsync()
        {
            var exercises = await _repo.GetAllExercisesAsync();
            List<ExerciseDTO> dtos = (await Task.WhenAll(exercises.Select(ConvertToDTO))).ToList(); 
            return dtos;
        }

        public async Task<List<ExerciseDTO>?> GetExerciseByEnum(AreaEnum area)
        {
            var exas = await GetExerciseByIdAsync((int)(area));
            return exas;
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

        public async Task<ExerciseDTO> GetExerciseByName(string name)
        {
            var ex = await _repo.GetExerciseByName(name);   
            if(ex == null)
            {
                return null;
            }
            return await ConvertToDTO(ex);
        }

        public async Task<List<ExerciseDTO>?> GetRandomExercisesAsync()
        {
            var dtos = await GetAllExercisesAsync();
            var arr = dtos.ToArray();
            var randoms = arr.Shuffle().Take(3).ToList();
            return randoms;
        }
        public async Task<ExerciseDTO> ConvertToDTO(Exercise exercise)
        {
           return new ExerciseDTO
            {
                Id = exercise.Id,
                VideoSourceLink = exercise.VideoSourceLink,
                Name = exercise.Name,

                TargetAreaNames = exercise.TargetAreas.
                Select(e => e.ExerciseCategoryName).
                ToList(),
            };
        }

        public async Task<bool> SaveExercise(ExerciseDTO newExercise)
        {
            var checkExisting = await GetExerciseByName(newExercise.Name);

            if (checkExisting != null)
            {
                return false;
            }

            var targetAreas = (await Task.WhenAll(newExercise.TargetAreaNames.Select(_traningAreaRepo.GetExerciseTargetAreaByName))).ToList();
            var domain = new Exercise
            {
                Name = newExercise.Name,
                VideoSourceLink = newExercise.VideoSourceLink,
                TargetAreas = targetAreas,
            };
           return await _repo.AddExercise(domain);
        }
    }
}
