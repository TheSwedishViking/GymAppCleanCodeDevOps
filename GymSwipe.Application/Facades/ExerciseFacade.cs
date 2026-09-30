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
            var exs = await  _exerciseService.GetAllExercisesAsync();
            //foreach(var ex in exs)
            //{
            //    var dto = new ExerciseDTO
            //    {
            //        Id = ex.Id,
            //        Name = ex.Name,
            //        VideoSourceLink = ex.VideoSourceLink,
            //        TargetAreaId = ex.TargetAreaId,
            //        TargetAreaName = ex.TargetAreaName,
            //    };
            //}
            return  exs ?? new List<ExerciseDTO>();
        }
    }
}
