using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<GymPlaylist> GetPlaylist(int id);


    }
}
