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
                Excercise = playlist.Exercises.Select(MapExercise).ToList()
            };
        }
        public static PlaylistExerciseDTO MapExercise(PlaylistExercise exercise)
        {
            return new PlaylistExerciseDTO
            {
                ExerciseId = exercise.Id,
                Name = exercise.Exercise.Name,
                VideoSourceLink = exercise.Exercise.VideoSourceLink ?? string.Empty,
                PlaylistOrder = exercise.PlaylistOrder
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


        public async Task<GymPlaylistDTO> GetTodaysPlaylist(int userId)
        {
            var domain = await _playlistRepo.GetTodaysPlaylistByUserId(userId, await _dateHandler.SetDateAsDateOnly());
            var dto = MapPlaylist(domain);
            return dto;
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
            var domain = DomainMapPlaylist(playlist);
            await _playlistRepo.UpdatePlaylist(domain);
        }

        private static GymPlaylist DomainMapPlaylist(GymPlaylistDTO playlist)
        {
            return new GymPlaylist
            {
                Id = playlist.Id,
                Name = playlist.Name,
                UserId = playlist.UserId,
                HasBeenPlayedBefore = true,
                CaloriesBurnedFromLatestWorkout = playlist.CaloriesBurnedFromLatestWorkout,
                DateCreated = playlist.DateCreated,
                TimesPlayed = playlist.TimesPlayed,
                LastPlayed = playlist.LastPlayed,
                LatestElapsedTime = playlist.LatestElapsedTime,
                //Exercises = playlist.Excercise.Select(DomainMapPlaylistExercise).ToList(),
            };
        }
        private static PlaylistExercise DomainMapPlaylistExercise(PlaylistExerciseDTO dto)
        {
            return new PlaylistExercise
            {
                ExerciseId = dto.ExerciseId,
                PlaylistOrder = dto.PlaylistOrder,
            };
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
