using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IWorkoutFacade
    {
        Task<PlaylistExercise> StartWorkout();
        Task PauseWorkout();
        Task CompleteWorkout();
        Task<PlaylistExercise> GetNextExercise(PlaylistExercise current);
        Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise current);
        Task<GymPlaylistDTO> GetPlaylistById(int id);
        Task<GymPlaylistDTO> GetPlaylistByUserId(int userId);
        Task<bool> QuitWorkout();
        Task RecordUserWorkout();
    }
}
