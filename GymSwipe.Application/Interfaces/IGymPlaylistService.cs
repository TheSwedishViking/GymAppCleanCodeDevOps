using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylist> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);
        Task<GymPlaylist> GetTodaysPlaylist(int userId);
        Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current, ICollection<PlaylistExcercise> excercise);
        Task<PlaylistExcercise> GetPreviousExercise(PlaylistExcercise current, ICollection<PlaylistExcercise> excercise);
        Task<PlaylistExcercise> GetFirstExercise(ICollection<PlaylistExcercise> excercise);
    }
}
