using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Repos
{
    public class TrainingAreaRepo : ITraningAreaRepo
    {
        private readonly GymAppDbContext _db;
        public TrainingAreaRepo(GymAppDbContext dbContext)
        {
            _db = dbContext;
        }
        public async Task AddTargetArea(ExerciseTargetArea area)
        {
            _db.ExercisesTargetAreas.Add(area);
            await _db.SaveChangesAsync();
        }

        public async Task<ExerciseTargetArea> GetExerciseTargetAreaByName(string name)
        {
            return await _db.ExercisesTargetAreas .FirstOrDefaultAsync(e => e.ExerciseCategoryName == name);
        }

        public async Task<List<ExerciseTargetArea>> GetExerciseTargetAreasAsync()
        {
            return await _db.ExercisesTargetAreas.ToListAsync() ?? new List<ExerciseTargetArea>();
        }

        public async Task RemoveTargetArea(int id)
        {
            var area = _db.ExercisesTargetAreas.FirstOrDefault(e=>e.Id == id);
            if(area == null)
            {
                return;
            }
            _db.ExercisesTargetAreas.Remove(area);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTargetArea(ExerciseTargetArea area)
        {
            var thisArea = _db.ExercisesTargetAreas.FirstOrDefault(e=>e.Id == area.Id);
            if(thisArea == null)
            {
                return;
            }
            throw new NotImplementedException();
        }
    }
}
