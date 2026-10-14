using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Controls
{
    public interface IPlaylistControls
    {
        Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercise);
        Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO current, List<PlaylistExerciseDTO> excercise);
        Task<PlaylistExerciseDTO> GetFirstExercise(List<PlaylistExerciseDTO> excercise);
    }
}
