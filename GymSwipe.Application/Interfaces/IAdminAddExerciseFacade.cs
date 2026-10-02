using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IAdminAddExerciseFacade
    {
        Task AddExercise(Exercise newExercise);
        Task<List<TargetAreaDTO>> GetExerciseTargetsAsync();
    }
}
