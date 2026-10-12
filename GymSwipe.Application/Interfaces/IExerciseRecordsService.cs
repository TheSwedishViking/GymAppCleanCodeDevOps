using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsService
    {
        Task<ExerciseRecord> GetAExerciseRecordsAsync(int exerciseId, int userId);
        Task SaveRecordsFromCurrentPlaylist(List<ExerciseRecordDTO> records);
    }
}
