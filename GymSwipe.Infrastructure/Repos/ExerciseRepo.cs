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

        public async Task AddExercise(Exercise domain)
        {
            _db.Exercises.Add
                (domain);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return  await _db.Exercises.
                Include(e=>e.TargetAreas).ToListAsync();
        }

        public async Task<Exercise> GetExerciseByName(string name)
        {
            return await _db.Exercises.FirstOrDefaultAsync(e=>e.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Exercise>> GetExercisesByCategoryAsync(int categoryId)
        {
            return await 
                    _db.Exercises.
                    Include(e=>e.TargetAreas).
                    Where(e=>e.TargetAreas.
                    Any
                    (e=>e.Id==categoryId)).OrderBy(n=>n.Id).
                    ToListAsync();
        }

       
    }
}
