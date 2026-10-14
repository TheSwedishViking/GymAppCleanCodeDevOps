using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IMapDomainService
    {
        Task<GymPlaylist> MapPlaylistDomain(GymPlaylistDTO gymPlaylist);
        Task<Exercise> MapExerciseDomain(ExerciseDTO exercise);
        Task<PlaylistExercise> MapPlaylistExerciseFromExercise(Exercise exercise);
        Task<PlaylistExercise> MapPlaylistExerciseDomain(PlaylistExerciseDTO playlistExercise, int playlistId);
        Task<GymUser> MapGymUserDomain(GymUserDTO gymUser);
        Task<ExerciseTargetArea> MapTargetAreaDomain(TargetAreaDTO exerciseTargetArea);
        Task<ExerciseRecord> MapExerciseRecordDomain(ExerciseRecordDTO exerciseRecord);
    }
}
