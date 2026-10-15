using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.Mappers
{
    public class MapDomainService : IMapDomainService
    {
        private IExerciseRepository _exerciseRepository;
        private IGymPlaylistRepository _gymPlaylistRepository;
        private ITraningAreaRepo _traningAreaRepo;
        public MapDomainService(
            IExerciseRepository exerciseRepository, 
            IGymPlaylistRepository gymPlaylistRepository,
            ITraningAreaRepo traningAreaRepo
            )
        {
            _exerciseRepository = exerciseRepository;
            _gymPlaylistRepository = gymPlaylistRepository;
            _traningAreaRepo = traningAreaRepo;

        }
        public async Task<Exercise> MapExerciseDomain(ExerciseDTO exercise)
        {
            var ids = exercise.TargetAreaNames.Select(e => e.Id);
            var areas = (await Task.WhenAll(ids.Select(_traningAreaRepo.GetAreaById))).ToList();
            return new Exercise
            {
                Id = exercise.Id,
                VideoSourceLink = exercise.VideoSourceLink,
                Name = exercise.Name,
                TargetAreas = areas
            };
        }

        public async Task<ExerciseRecord> MapExerciseRecordDomain(ExerciseRecordDTO exerciseRecord)
        {
            return new ExerciseRecord
            {
                Id = exerciseRecord.Id,
                ExerciseId = exerciseRecord.ExerciseId,
                UserId = exerciseRecord.UserId,
                WeightKg = exerciseRecord.WeightKg,
                Sets = exerciseRecord.Sets,
                Repetitions = exerciseRecord.Repetitions,
                DateRecorded = exerciseRecord.DateRecorded,
                UserRating = exerciseRecord.UserRating,
            };
        }

        public async Task<GymUser> MapGymUserDomain(GymUserDTO gymUser)
        {
           var user = new GymUser { 
                Id = gymUser.Id,
                Firstname = gymUser.Firstname,
                Surname = gymUser.Surname,
                Email = gymUser.Email,
                Gender = gymUser.Gender,
                HeightCm = gymUser.HeightCm,
                WeightKg = gymUser.WeightKg,
                //UserPlaylists = gymUser.pla
            };
            user.RestoreFriendCode(gymUser.FriendCode);
            return user;
        }

        public async Task<GymPlaylist> MapPlaylistDomain(GymPlaylistDTO gymPlaylist)
        {
            var exercises = new List<PlaylistExercise>();
            foreach (var e in gymPlaylist.Excercise)
            {
                exercises.Add(await MapPlaylistExerciseDomain(e, gymPlaylist.Id));
            }
            return new GymPlaylist
            {
                Id = gymPlaylist.Id,
                Name = gymPlaylist.Name,
                UserId = gymPlaylist.UserId,
                Exercises = exercises,
                CaloriesBurnedFromLatestWorkout = gymPlaylist.CaloriesBurnedFromLatestWorkout,
                LastPlayed = gymPlaylist.LastPlayed,
                LatestElapsedTime = gymPlaylist.LatestElapsedTime,
                HasBeenPlayedBefore=true,
            };
        }

        public async Task<PlaylistExercise> MapPlaylistExerciseDomain(PlaylistExerciseDTO playlistExercise, int playlistId)
        {
            var exercise = await _exerciseRepository.GetExerciseById(playlistExercise.ExerciseId);
            var playlist = await _gymPlaylistRepository.GetPlaylist(playlistId);
            return new PlaylistExercise
            {
                Id = exercise.Id,
                Exercise = exercise,
                ExerciseId = exercise.Id,
                PlaylistId = playlistId,
                Playlist = playlist,
                PlaylistOrder = playlistExercise.PlaylistOrder,
            };
        }

        public async Task<PlaylistExercise> MapPlaylistExerciseFromExercise(Exercise exercise)
        {
            return new PlaylistExercise
            {
                ExerciseId = exercise.Id,
            };
        }

        public async Task<ExerciseTargetArea> MapTargetAreaDomain(TargetAreaDTO exerciseTargetArea)
        {
            return new ExerciseTargetArea
            {
                Id = exerciseTargetArea.Id,
                ExerciseCategoryName = exerciseTargetArea.Name,
            };
        }
    }
}
