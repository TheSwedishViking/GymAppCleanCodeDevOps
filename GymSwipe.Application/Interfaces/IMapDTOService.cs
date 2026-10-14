using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IMapDTOService
    {
        Task<GymPlaylistDTO> MapPlaylistDTO(GymPlaylist gymPlaylist);
        Task<ExerciseDTO> MapExerciseDTO(Exercise exercise);
        Task<PlaylistExerciseDTO> MapPlaylistExerciseDTO(PlaylistExercise playlistExercise);
        Task<GymUserDTO> MapGymUserDTO(GymUser gymUser);
        Task<TargetAreaDTO> MapTargetAreaDTO(ExerciseTargetArea  exerciseTargetArea);
        Task<ExerciseRecordDTO> MapExerciseRecordDTO(ExerciseRecord exerciseRecord);
        Task<GymPlaylistDTO> CopyDomainFieldsPlaylistDTO(GymPlaylist existsAlready);
    }
}
