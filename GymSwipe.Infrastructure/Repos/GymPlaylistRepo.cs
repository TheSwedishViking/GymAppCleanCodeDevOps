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
                //Nothing for today, yesterdays instead? Random other? Create random? Food for though, thought for food
                var all = _db.GymPlaylists.
                    Where(p => p.UserId == userId)
                    .Include(p => p.Exercises.OrderBy(pr => pr.PlaylistOrder))
                    .ThenInclude(pl => pl.Exercise).ToArray();

                //return a random for now
                return all[Random.Shared.Next(all.Length)];
            }

            return playlist;
        }

        public async Task SavePlayList(GymPlaylist newPlaylist)
        {
            _db.GymPlaylists.Add(newPlaylist);
            await _db.SaveChangesAsync();
        }

        public async Task UpdatePlaylist(GymPlaylist playlistService)
        {
            var existing = await _db.GymPlaylists.FirstOrDefaultAsync(p=>p.Id == playlistService.Id);
            if (existing == null) return;

            existing.HasBeenPlayedBefore = playlistService.HasBeenPlayedBefore;
            existing.CaloriesBurnedFromLatestWorkout = playlistService.CaloriesBurnedFromLatestWorkout;
            existing.LastPlayed = playlistService.LastPlayed;
            existing.LatestElapsedTime = playlistService.LatestElapsedTime;

            await _db.SaveChangesAsync();
        }
    }
}
