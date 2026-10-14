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
        private readonly IAdminCreatorValidator _adminCreatorValidator;
        public AdminAddExerciseFacade(IExerciseService exerciseService, ITraningAreaService traningAreaService, IAdminCreatorValidator adminCreatorValidator)
        {
            _exerciseService = exerciseService;
            _traningAreaService = traningAreaService;
            _adminCreatorValidator = adminCreatorValidator;
        }
        public async Task<List<string>> AddExercise(ExerciseDTO newExercise)
        {
            List<string> possibleErros = new List<string>();
            //Validate
            if(newExercise == null)
            {
                possibleErros.Add("Error with reading exercise; validate fields");
                return possibleErros;
            }

            possibleErros = await _adminCreatorValidator.ValidateNewExercise(newExercise);
            if (possibleErros.Count > 0)
            {
                return possibleErros;
            }
            var checkExisting = await _exerciseService.GetExerciseByName(newExercise.Name);
            if(checkExisting == null)
            {
                await _exerciseService.SaveExercise(newExercise);
            }
            else
            {
                possibleErros.Add("Already exists");
            }
            return possibleErros;
          
        }

        public async Task<List<TargetAreaDTO>> GetExerciseTargetsAsync()
        {
            return await _traningAreaService.GetExerciseTargetsAsync();
        }
    }
}
