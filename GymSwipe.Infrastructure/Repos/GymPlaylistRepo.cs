using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.Infrastructure.Repos
{
    public class GymPlaylistRepo : IPlaylistRepository
    {
        private readonly IPlaylistRepository _db;

        public GymPlaylistRepo(IPlaylistRepository db)
        {
            _db = db;
        }


        public async Task<GymPlaylist> GetPlaylist(int id)
        {
            GymPlaylist foundPlaylist = await _db.GetPlaylist(id);
            if (foundPlaylist == null)
            {
                throw new Exception($"Playlist with id {id} not found.");
            }

            return foundPlaylist;
        }

    }
}
