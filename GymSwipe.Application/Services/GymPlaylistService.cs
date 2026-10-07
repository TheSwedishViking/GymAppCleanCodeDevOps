using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class GymPlaylistService : IGymPlaylistService
    {
        private readonly IGymPlaylistRepository _playlistRepo;
        private readonly IDateHandler _dateHandler;

        public GymPlaylistService(
            IGymPlaylistRepository playlistRepo, 
            IDateHandler dateHandler)
        {
            _playlistRepo = playlistRepo;
            _dateHandler = dateHandler;
        }

        public async Task<GymPlaylist> GetPlaylist(int id)
        {
            return await _playlistRepo.GetPlaylist(id);
        }
        public async Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId)
        {
            return await _playlistRepo.GetPlaylistsByUserId(userId);
        }

        public async Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser)
        {
            GymPlaylist newPlaylist = new GymPlaylist();

            for (int i = 0; i < addedExercises.Count; i++)
            {
                newPlaylist.Excercise.Add(ConvertToDomain(addedExercises[i], i));
            }
            newPlaylist.Name = await SetNameOfPlaylist(currentUser.Firstname);
            newPlaylist.UserId = currentUser.Id;
            newPlaylist.DateCreated = await _dateHandler.SetDateAsDateOnly();

            await _playlistRepo.SavePlayList(newPlaylist);
        }
        public async Task<string> SetNameOfPlaylist(string user)
        {
            return $"{user}'s gymplaylist {await _dateHandler.SetDateAsString()}";
        }
        private PlaylistExcercise ConvertToDomain(ExerciseDTO dTO, int order)
        {
            //Minimal needed to point towards correct entities without EF issues
            return new PlaylistExcercise
            {
                ExerciseId = dTO.Id,
                PlaylistOrder = order,
            };

        }
    }
}
