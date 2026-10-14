using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System.Diagnostics;

namespace GymSwipe.ApplicationLayer.Interfaces.Services
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylistDTO> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task<List<GymPlaylistDTO>> GetPlaylistDTOsByUserId(int userId);
        Task SavePlaylistToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);
        Task<GymPlaylistDTO> GetTodaysPlaylist(int userId);
        Task RecordedCompletedWorkout(int id, TimeSpan elapsedTime, double calories);
    }
}
