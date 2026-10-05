using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IPlaylistService
    {
        GymPlaylist GetPlaylist(int id);
    }
}
