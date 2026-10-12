using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class GymPlaylistService : IGymPlaylistService
    {
        private readonly IGymPlaylistRepository _playlistRepo;
        private readonly IDateHandler _dateHandler;
        LoggedInUser currentUser;

        public GymPlaylistService(
            IGymPlaylistRepository playlistRepo,
            IDateHandler dateHandler)
        {
            _playlistRepo = playlistRepo;
            _dateHandler = dateHandler;
        }

        public async Task<GymPlaylistDTO> GetPlaylist(int id)
        {
            var playlist = await _playlistRepo.GetPlaylist(id);
            return playlist == null ? null : MapPlaylist(playlist);
        }







        public async Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId)
        {
            return await _playlistRepo.GetPlaylistsByUserId(userId);
        }

        public async Task<List<GymPlaylistDTO>> GetPlaylistDTOsByUserId(int userId)
        {
            var playlists = await _playlistRepo.GetPlaylistsByUserId(userId);
            return playlists.Select(MapPlaylist).ToList();
        }




        private static GymPlaylistDTO MapPlaylist(GymPlaylist playlist)
        {
            return new GymPlaylistDTO
            {
                Id = playlist.Id,
                Name = playlist.Name,
                DateCreated = playlist.DateCreated,
                UserId = playlist.UserId,
                Excercise = playlist.Excercise.Select(exercise => new PlaylistExerciseDTO
                {
                    ExerciseId = exercise.ExerciseId,
                    Name = exercise.Exercise?.Name ?? string.Empty,
                    VideoSourceLink = exercise.Exercise?.VideoSourceLink ?? string.Empty,
                    PlaylistOrder = exercise.PlaylistOrder
                }).ToList()
            };
        }

        public async Task SavePlaylistToUser(List<ExerciseDTO> addedExercises, GymUser currentUser)
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
            return new PlaylistExcercise
            {
                ExerciseId = dTO.Id,
                PlaylistOrder = order,
            };

        }
    }
}
