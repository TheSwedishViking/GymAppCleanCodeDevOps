using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSwipe.Infrastructure.Repos
{
    public class GymPlaylistRepo : IPlaylistRepository
    {
        private readonly GymAppDbContext _db;

        public GymPlaylistRepo(GymAppDbContext db)
        {
            _db = db;
        }


        public async Task<GymPlaylist?> GetPlaylist(int id)
        {
            GymPlaylist? foundPlaylist = await _db.GymPlaylists.FirstOrDefaultAsync(playlist => playlist.Id == id);
            if (foundPlaylist == null)
            {
                return null;
            }

            return foundPlaylist;
        }

    }
}
