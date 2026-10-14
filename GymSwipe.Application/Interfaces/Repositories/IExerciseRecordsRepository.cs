using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Repositories
{
    public interface IExerciseRecordsRepository
    {
        Task SaveRecords(List<ExerciseRecord> records);
    }
}
