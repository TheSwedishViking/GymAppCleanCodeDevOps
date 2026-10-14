using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.Validators
{
    public class AdminCreatorValidator : IAdminCreatorValidator
    {
        public async Task<List<string>> ValidateNewExercise(ExerciseDTO newExercise)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(newExercise.Name))
            {
                errors.Add("Name for exercise is required");
            }
            if(newExercise.TargetAreaNames.Count == 0)
            {
                errors.Add("A new exercise should target something. Register a new one if there's no fitting one!");
            }
            return errors;
        }

        public async Task<List<string>> ValidateNewTargetArea(TargetAreaDTO newTargetArea)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(newTargetArea.Name))
            {
                errors.Add("Name for area is required");
            }
            return errors;
        }
    }
}
