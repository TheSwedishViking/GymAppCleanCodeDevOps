using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsService
    {
        Task<ExerciseRecords> GetAExerciseRecordsAsync(int exerciseId, int userId);
        Task SaveRecordsFromCurrentPlaylist(int userId, GymPlaylist usersPlaylist);
    }
}
