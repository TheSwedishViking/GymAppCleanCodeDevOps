using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymSwipe.Infrastructure.Repos
{
    public class GymPlaylistRepo : IGymPlaylistRepository
    {
        private readonly GymAppDbContext _db;

        public GymPlaylistRepo(GymAppDbContext db)
        {
            _db = db;
        }


        public async Task<GymPlaylist?> GetPlaylist(int id)
        {


            return await _db.GymPlaylists
                .Include(p => p.Excercise.OrderBy(pe => pe.PlaylistOrder))
                .ThenInclude(pe => pe.Exercise)
                .FirstOrDefaultAsync(p => p.Id == id);



        }

    }
}
