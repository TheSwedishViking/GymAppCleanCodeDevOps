using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IExerciseRecordsFacade
    {
        Task<bool> GetActiveStatus();
        Task<List<PlaylistExcercise>> GetPlaylistExercises();
        Task<PlaylistExcercise> GetNextExercise(ExerciseDTO current);
        Task<bool> SaveRecords(List<ExerciseRecordDTO> records);
        Task AddRecord(ExerciseRecordDTO currentRecord);
    }
}
