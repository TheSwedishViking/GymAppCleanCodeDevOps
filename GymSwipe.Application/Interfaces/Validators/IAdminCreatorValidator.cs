using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Validators
{
    public interface IAdminCreatorValidator
    {
        Task<List<string>> ValidateNewTargetArea(TargetAreaDTO newTargetArea);
        Task<List<string>> ValidateNewExercise(ExerciseDTO newExercise);
    }
}
