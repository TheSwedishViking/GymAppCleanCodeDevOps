using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class AdminAddExerciseFacade : IAdminAddExerciseFacade
    {
        private readonly IExerciseService _exerciseService;
        private readonly ITraningAreaService _traningAreaService;
        public AdminAddExerciseFacade(IExerciseService exerciseService, ITraningAreaService traningAreaService)
        {
            _exerciseService = exerciseService;
            _traningAreaService = traningAreaService;
        }
        public async Task<bool> AddExercise(ExerciseDTO newExercise)
        {
            //Validate
            if(newExercise == null)
            {
                throw new Exception("Empty exercise, can''t register"); 
            }

            var checkExisting = await _exerciseService.GetExerciseByName(newExercise.Name);
            if(checkExisting == null)
            {
                await _exerciseService.SaveExercise(newExercise);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<TargetAreaDTO>> GetExerciseTargetsAsync()
        {
            return await _traningAreaService.GetExerciseTargetsAsync();
        }
    }
}
