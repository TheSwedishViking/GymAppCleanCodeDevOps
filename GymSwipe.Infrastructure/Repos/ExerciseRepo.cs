using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSwipe.Infrastructure.Repos
{
    public class ExerciseRepo : IExerciseRepository
    {
        private readonly GymAppDbContext _db;
        public ExerciseRepo(GymAppDbContext gymAppDbContext)
        {
            _db = gymAppDbContext;
        }

        public async Task<Exercise> GetExerciseById(int id)
        {
            var ex =await _db.Exercises.
                Include(e=>e.TargetAreas).FirstOrDefaultAsync(e=>e.Id==id);
            if(ex == null)
            {
                throw new Exception("Did not find exercise by that Id");
            }
            return ex;
        }

        public async Task<bool> AddExercise(Exercise domain)
        {
            _db.Exercises.Add
                (domain);

            await _db.SaveChangesAsync();
            return true;
        }


        public async Task<List<Exercise>> GetAllExercisesAsync()
        {
            return await _db.Exercises.
                Include(e => e.TargetAreas).ToListAsync();
        }

      
        public async Task<Exercise> GetExerciseByName(string name)
        {
            return await _db.Exercises.FirstOrDefaultAsync(e => e.Name.ToLower() == name.ToLower());
        }

        public async Task<List<Exercise>> GetExercisesByCategoryAsync(int categoryId)
        {
            return await
                    _db.Exercises.
                    Include(e => e.TargetAreas).
                    Where(e => e.TargetAreas.
                    Any
                    (e => e.Id == categoryId)).OrderBy(n => n.Id).
                    ToListAsync();
        }


    }
}
