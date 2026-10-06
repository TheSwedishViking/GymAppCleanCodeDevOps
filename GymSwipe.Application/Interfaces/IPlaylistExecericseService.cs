using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistExecericseService
    {
        Task<List<PlaylistExcercise>> GetExercisesByPlaylistIdAsync(int playlistId);

    }
}
