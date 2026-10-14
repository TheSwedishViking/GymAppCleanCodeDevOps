using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class MapDTOService : IMapDTOService
    {
        public async Task<GymPlaylistDTO> CopyDomainFieldsPlaylistDTO(GymPlaylist existsAlready)
        {
            var dtoExercises = (await Task.WhenAll(existsAlready.Exercises.Select(MapPlaylistExerciseDTO))).ToList();

            return new GymPlaylistDTO
            {
                Id = existsAlready.Id,
                Name = existsAlready.Name,
                UserId = existsAlready.UserId,
                CaloriesBurnedFromLatestWorkout = existsAlready.CaloriesBurnedFromLatestWorkout,
                HasBeenPlayedBefore = existsAlready.HasBeenPlayedBefore,
                DateCreated = existsAlready.DateCreated,
                LastPlayed = existsAlready.LastPlayed,
                TimesPlayed = existsAlready.TimesPlayed,
                LatestElapsedTime = existsAlready.LatestElapsedTime,
                Excercise = dtoExercises
            };
        }
        public async Task<ExerciseDTO> MapExerciseDTO(Exercise exercise)
        {
            var targetAreas =  (await Task.WhenAll(exercise.TargetAreas.Select(MapTargetAreaDTO))).ToList();
            return new ExerciseDTO
            {
                Id = exercise.Id,
                VideoSourceLink = exercise.VideoSourceLink,
                Name = exercise.Name,
                //Look over if we need string or TargetAreaDTO in the future
                TargetAreaNames = targetAreas
            };
        }

        public async Task<ExerciseRecordDTO> MapExerciseRecordDTO(ExerciseRecord exerciseRecord)
        {
            return new ExerciseRecordDTO
            {
                Id= exerciseRecord.Id,
                UserId = exerciseRecord.UserId,
                Sets = exerciseRecord.Sets,
                DateRecorded = exerciseRecord.DateRecorded,
                ExerciseId = exerciseRecord.Id,
                Repetitions = exerciseRecord.Repetitions,
                UserRating = exerciseRecord.UserRating,
                WeightKg = exerciseRecord.WeightKg,
            };
        }

        public async Task<GymUserDTO> MapGymUserDTO(GymUser gymUser)
        {
            return new GymUserDTO
            {
                Id = gymUser.Id,
                Firstname = gymUser.Firstname,
                Surname = gymUser.Surname,
                Email = gymUser.Email,
                FriendCode = gymUser.FriendCode,
                Gender = gymUser.Gender,
                HeightCm = gymUser.HeightCm,
                WeightKg = gymUser.WeightKg,
            };
        }

        public async Task<GymPlaylistDTO> MapPlaylistDTO(GymPlaylist gymPlaylist)
        {
            var dtoExercises = (await Task.WhenAll(gymPlaylist.Exercises.Select(MapPlaylistExerciseDTO))).ToList();

            return new GymPlaylistDTO
            {
                Id = gymPlaylist.Id,
                Name = gymPlaylist.Name,
                Excercise = dtoExercises,
                CaloriesBurnedFromLatestWorkout = gymPlaylist.CaloriesBurnedFromLatestWorkout,
                DateCreated = gymPlaylist.DateCreated,
                HasBeenPlayedBefore = gymPlaylist.HasBeenPlayedBefore,
                LastPlayed = gymPlaylist.LastPlayed,
                LatestElapsedTime = gymPlaylist.LatestElapsedTime,
                UserId = gymPlaylist.UserId,
                TimesPlayed = gymPlaylist.TimesPlayed
            };
        }

        public async Task<PlaylistExerciseDTO> MapPlaylistExerciseDTO(PlaylistExercise playlistExercise)
        {
            return new PlaylistExerciseDTO
            {
                Name = playlistExercise.Exercise.Name,
                ExerciseId = playlistExercise.ExerciseId,
                VideoSourceLink = playlistExercise.Exercise.VideoSourceLink,
                PlaylistOrder = playlistExercise.PlaylistOrder
            };
        }

        public async Task<TargetAreaDTO> MapTargetAreaDTO(ExerciseTargetArea exerciseTargetArea)
        {
            return new TargetAreaDTO
            {
                Id = exerciseTargetArea.Id,
                Name = exerciseTargetArea.ExerciseCategoryName
            };
        }
    }
}
