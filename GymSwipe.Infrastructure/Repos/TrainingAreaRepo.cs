using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GymSwipe.Infrastructure.Repos
{
    public class TrainingAreaRepo : ITraningAreaRepo
    {
        private readonly GymAppDbContext _db;
        public TrainingAreaRepo(GymAppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task<bool> AddTargetArea(ExerciseTargetArea area)
        {
            _db.ExercisesTargetAreas.Add(area);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<ExerciseTargetArea> GetAreaById(int id)
        {
            var area =  _db.ExercisesTargetAreas.FirstOrDefault(a=>a.Id == id);
            if(area == null)
            {
                return null;
            }
            return area;
        }

        public async Task<ExerciseTargetArea> GetExerciseTargetAreaByName(string name)
        {
            return await _db.ExercisesTargetAreas.FirstOrDefaultAsync(e => e.ExerciseCategoryName == name);
        }

        public async Task<List<ExerciseTargetArea>> GetExerciseTargetAreasAsync()
        {
            return await _db.ExercisesTargetAreas.ToListAsync() ?? new List<ExerciseTargetArea>();
        }

        public async Task<string> GetTargetAreaImage(int exerciseId)
        {

            var imagePath = await _db.Exercises
                            .Where(exercise => exercise.Id == exerciseId)
                            .SelectMany(exercise => exercise.TargetAreas) // Add collection, then sort
                            .Select(area => area.ExerciseCategoryName)
                            .FirstOrDefaultAsync();
            if (imagePath.IsNullOrEmpty())
            {
                return imagePath = "unknown"; //return the name ínstead of imagename

            }
            string[] splitName = imagePath.Split(" ");
            string correctName = string.Join("_", splitName);

            return string.IsNullOrEmpty(imagePath) ? "unknown" : correctName.ToLower(); //return the name ínstead of imagename
        }


        public async Task RemoveTargetArea(int id)
        {
            var area = _db.ExercisesTargetAreas.FirstOrDefault(e => e.Id == id);
            if (area == null)
            {
                return;
            }
            _db.ExercisesTargetAreas.Remove(area);
            await _db.SaveChangesAsync();
        }

      
    }
}
