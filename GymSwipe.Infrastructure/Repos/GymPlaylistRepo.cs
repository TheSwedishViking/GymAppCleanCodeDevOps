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
                .Include(p => p.Exercises.OrderBy(pe => pe.PlaylistOrder))
                .ThenInclude(pe => pe.Exercise)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId)
        {
            return await _db.GymPlaylists
                .Where(p => p.UserId == userId)
                .Include(p => p.Exercises.OrderBy(pe => pe.PlaylistOrder))
                    .ThenInclude(pe => pe.Exercise)
                .ToListAsync();
        }

        public async Task<GymPlaylist> GetTodaysPlaylistByUserId(int userId, DateOnly todaysDate)
        {
            var playlist =  _db.GymPlaylists
                .Where(p => p.UserId == userId)
                .Include(p => p.Exercises.OrderBy(pr => pr.PlaylistOrder))
                .ThenInclude(pl => pl.Exercise)
                .FirstOrDefault(today => today.DateCreated == todaysDate);

            if(playlist == null)
            {
                return null;
            }

            return playlist;
        }

        public async Task SavePlayList(GymPlaylist newPlaylist)
        {
            _db.GymPlaylists.Add(newPlaylist);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePlaylist(GymPlaylist playlist)
        {
            var existing = await _db.GymPlaylists.FirstOrDefaultAsync(p=>p.Id == playlist.Id);
            if (existing == null) return;

            existing.HasBeenPlayedBefore = playlist.HasBeenPlayedBefore;
            existing.CaloriesBurnedFromLatestWorkout = playlist.CaloriesBurnedFromLatestWorkout;
            existing.LastPlayed = playlist.LastPlayed;
            existing.LatestElapsedTime = playlist.LatestElapsedTime;
            existing.TimesPlayed = playlist.TimesPlayed;
            await _db.SaveChangesAsync();
        }
    }
}
