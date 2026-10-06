using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistExecericseService
    {
        Task<List<PlaylistExcercise>> GetExercisesByPlaylistIdAsync(int playlistId);
        Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser);
    }
}
