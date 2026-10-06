using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class GymPlaylistService : IGymPlaylistService
    {


        private readonly IGymPlaylistRepository _repo;

        public GymPlaylistService(IGymPlaylistRepository repo)
        {
            _repo = repo;
        }

        public async Task<GymPlaylist> GetPlaylist(int id)
        {
            return await _repo.GetPlaylist(id);
        }
        public async Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId)
        {
            return await _repo.GetPlaylistsByUserId(userId);
        }
    }
}
