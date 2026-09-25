using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Models
{
    public class PlaylistExcercise
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int PlaylistId { get; set; }
        public int PlaylistOrder {  get; set; }
    }
}
