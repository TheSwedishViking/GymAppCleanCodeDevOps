using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Repos
{
    public class ExerciseRecordsRepository : IExerciseRecordsRepository
    {
        private readonly GymAppDbContext _db;
        public ExerciseRecordsRepository(GymAppDbContext gymAppDbContext)
        {
            _db = gymAppDbContext;
        }
        public Task<ExerciseRecord> GetRecordsById(int exerciseId, int userId)
        {
            throw new NotImplementedException();
        }
        public async Task SaveRecords(List<ExerciseRecord> records)
        {
            foreach(var record in records)
            {
                _db.ExerciseRecords.Add(record);
            }
            await _db.SaveChangesAsync();
        }
    }
}
