using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsRepository
    {
        Task<ExerciseRecords> GetRecordsById(int exerciseId, int userId);
        Task SaveRecords(ExerciseRecords records);
    }
}
