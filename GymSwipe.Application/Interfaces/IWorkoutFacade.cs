using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IWorkoutFacade
    {
        Task<PlaylistExcercise> StartWorkout();
        Task PauseWorkout();
        Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current);
        Task<PlaylistExcercise> GetPreviousExercise(PlaylistExcercise current);
        Task<GymPlaylist> GetPlaylistById(int id);
        Task<GymPlaylist> GetPlaylistByUserId(int userId);
        Task<bool> QuitWorkout();
        Task RecordUserWorkout();
    }
}
