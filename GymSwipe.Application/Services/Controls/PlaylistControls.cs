using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services.Controls
{
    public class PlaylistControls : IPlaylistControls
    {
        public async Task<PlaylistExerciseDTO> GetFirstExercise(List<PlaylistExerciseDTO> excercise)
        {
            return excercise.OrderBy(e => e.PlaylistOrder).FirstOrDefault();
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
    }
}
