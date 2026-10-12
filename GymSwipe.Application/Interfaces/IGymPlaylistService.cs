using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylistDTO> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task<List<GymPlaylistDTO>> GetPlaylistDTOsByUserId(int userId);
        Task SavePlaylistToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);

    }
}
