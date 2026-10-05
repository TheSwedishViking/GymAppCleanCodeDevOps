using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IGymPlaylistRepository
    {
        Task<GymPlaylist> GetPlaylist(int id);


    }
}
