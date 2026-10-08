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
        public Task<ExerciseRecords> GetRecordsById(int exerciseId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveRecords(ExerciseRecords records)
        {
            throw new NotImplementedException();
        }
    }
}
