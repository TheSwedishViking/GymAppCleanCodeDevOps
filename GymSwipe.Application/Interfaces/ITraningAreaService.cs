using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITraningAreaService
    {
        Task<List<ExerciseTargetArea>> GetExerciseTargetsAsync();
    }
}
