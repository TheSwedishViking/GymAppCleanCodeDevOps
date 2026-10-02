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
        public Task AddExercise(Exercise newExercise)
        {
            //Validate

            //Convert to DTO

            //Send over API (if everythigns valid)

            throw new NotImplementedException();
        }

        public Task<List<ExerciseTargetArea>> GetExerciseTargetsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
