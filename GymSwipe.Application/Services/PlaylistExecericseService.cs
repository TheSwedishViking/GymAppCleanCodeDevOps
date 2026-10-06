using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class PlaylistExecericseService : IPlaylistExecericseService
    {
        private readonly IPlaylistExecericseRepository _repo;

        public PlaylistExecericseService(IPlaylistExecericseRepository repo)
        {
            _repo = repo;
        }


        public async Task<List<PlaylistExcercise>> GetExercisesByPlaylistIdAsync(int playlistId)
        {
            return await _repo.GetExercisesByPlaylistIdAsync(playlistId);
        }
    }
}
