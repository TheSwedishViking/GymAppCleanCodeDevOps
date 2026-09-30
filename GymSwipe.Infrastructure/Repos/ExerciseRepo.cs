using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Repos
{
    public class ExerciseRepo : IExerciseRepository
    {
        private readonly GymAppDbContext _db;
        public ExerciseRepo(GymAppDbContext gymAppDbContext)
        {
            _db = gymAppDbContext;
        }
        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return  await _db.Exercises.ToListAsync();
        }

        public Task<List<IExerciseRepository>> GetExercisesByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
