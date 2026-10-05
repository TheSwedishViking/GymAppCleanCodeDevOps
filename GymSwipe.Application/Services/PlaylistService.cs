using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class PlaylistService : IPlaylistService
    {


        private readonly IPlaylistRepository _repo;

        public PlaylistService(IPlaylistRepository repo)
        {
            _repo = repo;
        }

        public async Task<GymPlaylist> GetPlaylist(int id)
        {
            return await _repo.GetPlaylist(id);
        }
    }
}
