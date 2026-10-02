using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class ExerciseFacade : IExerciseFacade
    {
        private readonly IExerciseService _exerciseService;
        private readonly IUserService _userService;
        public ExerciseFacade( IExerciseService exerciseService, IUserService userService   )
        {
            _exerciseService = exerciseService;
            _userService = userService;
        }

        public async Task<List<ExerciseDTO>> GetAllExercisesAsync()
        {
            var exs = await _exerciseService.GetAllExercisesAsync();

            return exs ?? new List<ExerciseDTO>();
        }

        public async Task<List<ExerciseDTO>> GetChestExerciesAsync(int id)
        {
            var chests = await _exerciseService.GetExerciseByIdAsync(id);
            return chests ?? new List<ExerciseDTO>();
        }

        public Task<ExerciseDTO> GetExerciseAsyncById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExerciseDTO>> GetExerciseListByCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExerciseDTO>> GetRandomExercisesAsync()
        {
            var exs = await  _exerciseService.GetRandomExercisesAsync();
          
            return  exs ?? new List<ExerciseDTO>();
        }
    }
}
