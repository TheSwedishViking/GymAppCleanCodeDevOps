using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistRepository
    {
        Task<GymPlaylist> GetPlaylist(int id);
        Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId);
        Task SavePlayList(GymPlaylist newPlaylist);

    }
}
