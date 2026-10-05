using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistService
    {
        Task<GymPlaylist> GetPlaylist(int id);
    }
}
