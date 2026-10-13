using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System.Diagnostics;

namespace GymSwipe.ApplicationLayer.Services
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
            var playlist = await _playlistRepo.GetPlaylist(id);
            if(playlist == null)
            {
                throw new Exception("No playlist found");
            }
            var dto = await _mapDTOService.MapPlaylistDTO(playlist);

            return dto;
        }
        public async Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercises)
        {
            var list = excercises.ToList();
            var index = list.FindIndex(e => e.PlaylistOrder == current.PlaylistOrder);
            if (index < 0)
            {
                return current;
            }
            if (index + 1 >= excercises.Count)
            {
                return null;
            }
            var newEx = list[index + 1];
            return newEx;
        }
        public async Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercise)
        {
            var list = excercise.ToList();
            var index = list.FindIndex(e => e.PlaylistOrder == current.PlaylistOrder);
            if (index - 1 < 0)
            {
                return current;
            }
            var oldEx = list[index - 1];
            return oldEx;
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
                throw new Exception("No playlists found");
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
            newPlaylist.DateCreated = await _dateHandler.SetDateAsDateOnly();
            await _playlistRepo.SavePlayList(newPlaylist);
        }

        public async Task<GymPlaylistDTO> GetTodaysPlaylist(int userId)
        {
            var domain = await _playlistRepo.GetTodaysPlaylistByUserId(userId, await _dateHandler.SetDateAsDateOnly());
            var dto = await _mapDTOService.MapPlaylistDTO(domain);
            return dto;
        }

       
        public async Task<string> SetNameOfPlaylist(string user)
        {
            return $"{user}'s gymplaylist {await _dateHandler.SetDateAsString()}";
        }

       
        public async Task<PlaylistExerciseDTO> GetFirstExercise(List<PlaylistExerciseDTO> excercise)
        {
            return  excercise.OrderBy(e=>e.PlaylistOrder).FirstOrDefault();
        }

        public async Task UpdatePlaylistInfo(GymPlaylistDTO playlist)
        {
            var existsAlready = await _playlistRepo.GetPlaylist(playlist.Id);
            if(existsAlready == null)
            {
               //New info? Add? Think over
            }
            var domain = await _mapDomainService.MapPlaylistDomain(playlist);
            await _playlistRepo.UpdatePlaylist(domain);
        }
        public async Task<TimeSpan> SetTime(Stopwatch stopwatch)
        {
            return  TimeSpan.FromMinutes(stopwatch.Elapsed.TotalMinutes);
        }
        public Task<DateTime> SetDate()
        {
            return _dateHandler.SetDateAsDateTime();
        }

        public async Task<int> IncrimentTimesPlayed(int timesPlayed)
        {
            return timesPlayed+=1;
        }
    }
}
