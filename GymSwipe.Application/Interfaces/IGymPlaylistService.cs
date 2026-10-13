using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System.Diagnostics;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylistDTO> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task<List<GymPlaylistDTO>> GetPlaylistDTOsByUserId(int userId);
        Task SavePlaylistToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);
        Task<GymPlaylistDTO> GetTodaysPlaylist(int userId);
        Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercise);
        Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercise);
        Task<PlaylistExerciseDTO> GetFirstExercise(List<PlaylistExerciseDTO> excercise);
        Task UpdatePlaylistInfo(GymPlaylistDTO playlistService);
        Task<TimeSpan> SetTime(Stopwatch stopwatch);
        Task<DateTime> SetDate();
        Task<int> IncrimentTimesPlayed(int timesPlayed);
    }
}
