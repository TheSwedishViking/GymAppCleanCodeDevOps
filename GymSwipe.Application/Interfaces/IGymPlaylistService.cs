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

        Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);
        Task<GymPlaylist> GetTodaysPlaylist(int userId);
        Task<PlaylistExercise> GetNextExercise(PlaylistExercise current, ICollection<PlaylistExercise> excercise);
        Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise current, ICollection<PlaylistExercise> excercise);
        Task<PlaylistExercise> GetFirstExercise(ICollection<PlaylistExercise> excercise);
        Task UpdatePlaylistInfo(GymPlaylist playlistService);
        Task<TimeSpan> SetTime(Stopwatch stopwatch);
        Task<DateTime> SetDate();
        Task<int> IncrimentTimesPlayed(int timesPlayed);
    }
}
