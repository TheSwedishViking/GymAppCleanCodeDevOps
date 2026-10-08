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
        Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current);
        Task<bool> SaveRecords(List<ExerciseRecords> records);
    }
}
