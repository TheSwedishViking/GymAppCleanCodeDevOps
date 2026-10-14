using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsRepository
    {
        Task SaveRecords(List<ExerciseRecord> records);
        Task<List<ExerciseRecord>> GetLatestRecords(int exerciseId, int userId);
        Task 
    }
}
