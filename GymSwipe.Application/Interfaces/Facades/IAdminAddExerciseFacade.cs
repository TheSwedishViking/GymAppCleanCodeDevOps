using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Facades
{
    public interface IAdminAddExerciseFacade
    {
        Task<List<string>> AddExercise(ExerciseDTO newExercise);
        Task<List<TargetAreaDTO>> GetExerciseTargetsAsync();
    }
}
