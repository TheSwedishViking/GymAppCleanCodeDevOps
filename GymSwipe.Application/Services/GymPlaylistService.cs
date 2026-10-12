using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System.Diagnostics;

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

        public async Task<GymPlaylistDTO> GetPlaylist(int id)
        {
            var playlist = await _playlistRepo.GetPlaylist(id);
            return playlist == null ? null : MapPlaylist(playlist);
        }
        public async Task<PlaylistExercise> GetNextExercise(PlaylistExercise current, ICollection<PlaylistExercise> excercises)
        {
            var list = excercises.ToList();
            var index = list.FindIndex(e => e.Id == current.Id);
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
        public async Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise current, ICollection<PlaylistExercise> excercise)
        {
            var list = excercise.ToList();
            var index = list.FindIndex(e => e.Id == current.Id);
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
                Excercise = playlist.Exercises.Select(exercise => new PlaylistExerciseDTO
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
                newPlaylist.Exercises.Add(ConvertToDomain(addedExercises[i], i));
            }
            newPlaylist.Name = await SetNameOfPlaylist(currentUser.Firstname);
            newPlaylist.UserId = currentUser.Id;
            newPlaylist.DateCreated = await _dateHandler.SetDateAsDateOnly();

            await _playlistRepo.SavePlayList(newPlaylist);
        }


        public async Task<GymPlaylist> GetTodaysPlaylist(int userId)
        {
            return await _playlistRepo.GetTodaysPlaylistByUserId(userId, await _dateHandler.SetDateAsDateOnly());
        }

        public async Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser)
        {
            GymPlaylist newPlaylist = new GymPlaylist();

            for (int i = 0; i < addedExercises.Count; i++)
            {
                newPlaylist.Exercises.Add(ConvertToDomain(addedExercises[i], i));
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

       
        private PlaylistExercise ConvertToDomain(ExerciseDTO dTO, int order)
        {
            //Minimal needed to point towards correct entities without EF issues
            return new PlaylistExercise
            {
                ExerciseId = dTO.Id,
                PlaylistOrder = order,
            };

        }
        public async Task<PlaylistExercise> GetFirstExercise(ICollection<PlaylistExercise> excercise)
        {
            return  excercise.OrderBy(e=>e.PlaylistOrder).FirstOrDefault();
        }

        public async Task UpdatePlaylistInfo(GymPlaylist playlistService)
        {
            var existsAlready = await _playlistRepo.GetPlaylist(playlistService.Id);
            if(existsAlready == null)
            {
                throw new Exception("Can't find playlist to update; error");
            }
            await _playlistRepo.UpdatePlaylist(playlistService);
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
