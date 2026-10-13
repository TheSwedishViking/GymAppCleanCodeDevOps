using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IWorkoutFacade
    {
        Task<PlaylistExerciseDTO> StartWorkout();
        Task PauseWorkout();
        Task CompleteWorkout();
        Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current);
        Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO current);
        Task<GymPlaylistDTO> GetPlaylistById(int id);
        Task<GymPlaylistDTO> GetPlaylistByUserId(int userId);
        Task<bool> QuitWorkout();
        Task RecordUserWorkout();
        Task<string> GetApproptiateImageForExercise(int exerciseId);
    }
}
