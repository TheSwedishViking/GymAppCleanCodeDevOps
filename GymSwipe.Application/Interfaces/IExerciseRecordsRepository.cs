using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsRepository
    {
        Task<ExerciseRecord> GetRecordsById(int exerciseId, int userId);
        Task SaveRecords(List<ExerciseRecord> records);
    }
}
