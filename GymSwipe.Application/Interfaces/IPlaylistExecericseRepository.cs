using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistExecericseRepository
    {
        Task<List<PlaylistExercise>> GetExercisesByPlaylistIdAsync(int playlistId);
        Task SavePlayList(GymPlaylist newPlaylist);
    }
}
