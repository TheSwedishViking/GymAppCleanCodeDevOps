using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Services
{
    public interface IExerciseRecordsService
    {
        Task SaveRecordsFromCurrentPlaylist(List<ExerciseRecordDTO> records);
    }
}
