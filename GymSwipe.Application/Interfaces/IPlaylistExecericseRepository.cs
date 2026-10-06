using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistExecericseRepository
    {
        Task<List<PlaylistExcercise>> GetExercisesByPlaylistIdAsync(int playlistId);
    }
}
