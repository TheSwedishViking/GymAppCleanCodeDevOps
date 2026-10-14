using GymSwipe.ApplicationLayer.Interfaces.Repositories;
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
