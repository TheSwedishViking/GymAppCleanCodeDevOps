using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class PlaylistService : IPlaylistService
    {
        public GymPlaylist GetPlaylist(int id)
        {
            return await _repo.GetPlaylist(id);
        }
    }
}
