using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.Domain.Models;
using System.Diagnostics;

namespace GymSwipe.ApplicationLayer.Services.Services
{
    public class GymPlaylistService : IGymPlaylistService
    {
        private readonly IGymPlaylistRepository _playlistRepo;
        private readonly IMapDomainService _mapDomainService;
        private readonly IMapDTOService _mapDTOService;
        private readonly IDateHandler _dateHandler;
        public GymPlaylistService(
            IGymPlaylistRepository playlistRepo,
            IMapDTOService mapDTOService,
            IMapDomainService mapDomainService,
            IDateHandler dateHandler)
        {
            _playlistRepo = playlistRepo;
            _dateHandler = dateHandler;
            _mapDomainService = mapDomainService;
            _mapDTOService = mapDTOService;
        }

        public async Task<GymPlaylistDTO> GetPlaylist(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid Id");
            }
            var playlist = await _playlistRepo.GetPlaylist(id);
            if(playlist == null)
            {
                throw new KeyNotFoundException("No playlist found");
            }
            var dto = await _mapDTOService.MapPlaylistDTO(playlist);

            return dto;
        }
        public async Task<List<GymPlaylist>> GetPlaylistsByUserId(int userId)
        {
            return await _playlistRepo.GetPlaylistsByUserId(userId);
        }
        public async Task<List<GymPlaylistDTO>> GetPlaylistDTOsByUserId(int userId)
        {
            var playlists = await _playlistRepo.GetPlaylistsByUserId(userId);
            if(playlists == null)
            {
                //User might have no playlists
                return null;
            }
            var dtos = (await Task.WhenAll(playlists.Select(_mapDTOService.MapPlaylistDTO))).ToList();
            return dtos;
        }
        //Save playlist with bare essentials, EF figures relations out with Ids
        public async Task SavePlaylistToUser(List<ExerciseDTO> addedExercises, GymUser currentUser)
        {
            GymPlaylist newPlaylist = new GymPlaylist();

            for (int i = 0; i < addedExercises.Count; i++)
            {
                var ex = await _mapDomainService.MapExerciseDomain(addedExercises[i]);
                var playEx = await _mapDomainService.MapPlaylistExerciseFromExercise(ex);
                playEx.PlaylistOrder = i;
                newPlaylist.Exercises.Add(playEx);
            }
            newPlaylist.Name = await SetNameOfPlaylist(currentUser.Firstname);
            newPlaylist.UserId = currentUser.Id;
            newPlaylist.DateCreated = _dateHandler.SetDateAsDateOnly();
            await _playlistRepo.SavePlayList(newPlaylist);
        }

        public async Task<GymPlaylistDTO> GetTodaysPlaylist(int userId)
        {
            var domain = await _playlistRepo.GetTodaysPlaylistByUserId(userId, _dateHandler.SetDateAsDateOnly());
            var dto = await _mapDTOService.MapPlaylistDTO(domain);
            return dto;
        }

       
        public async Task<string> SetNameOfPlaylist(string user)
        {
            return $"{user}'s gymplaylist { _dateHandler.SetDateAsString()}";
        }
    
        public async Task RecordedCompletedWorkout(int id, TimeSpan elapsedTime, double calories)
        {
            var playlist = await _playlistRepo.GetPlaylist(id)  ?? throw new InvalidOperationException("Can't find stored playlist");

            playlist.LatestElapsedTime = elapsedTime;
            playlist.LastPlayed = _dateHandler.SetDateAsDateTime();
            playlist.TimesPlayed++;
            playlist.CaloriesBurnedFromLatestWorkout = calories;

            await _playlistRepo.UpdatePlaylist(playlist);
        }
    }
}
