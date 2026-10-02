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
        public AdminAddExerciseFacade(IExerciseService exerciseService  )
        {
            _exerciseService = exerciseService;
        }
        public Task AddExercise(Exercise newExercise)
        {
            //Validate

            //Convert to DTO

            //Send over API (if everythigns valid)

            throw new NotImplementedException();
        }
    }
}
