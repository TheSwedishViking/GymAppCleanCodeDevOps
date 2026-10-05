using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistService
    {
        Task<GymPlaylist> GetPlaylist(int id);
    }
}
