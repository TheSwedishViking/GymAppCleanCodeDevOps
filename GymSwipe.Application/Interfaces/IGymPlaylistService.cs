using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylist> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);

    }
}
