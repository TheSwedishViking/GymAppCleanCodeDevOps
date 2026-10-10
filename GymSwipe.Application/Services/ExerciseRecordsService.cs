using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseRecordsService : IExerciseRecordsService
    {
        public Task<ExerciseRecords> GetAExerciseRecordsAsync(int exerciseId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task SaveRecordsFromCurrentPlaylist(int userId, GymPlaylist usersPlaylist)
        {
            throw new NotImplementedException();
        }
    }
}
