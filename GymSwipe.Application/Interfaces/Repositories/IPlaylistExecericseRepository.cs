using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces.Repositories
{
    public interface IPlaylistExecericseRepository
    {
        Task<List<PlaylistExercise>> GetExercisesByPlaylistIdAsync(int playlistId);
        Task SavePlayList(GymPlaylist newPlaylist);
    }
}
