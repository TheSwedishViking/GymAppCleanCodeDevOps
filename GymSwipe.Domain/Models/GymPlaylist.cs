using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Domain.Models
{
    public class GymPlaylist
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateCreated { get; set; }
        public int UserId { get; set; }
        public GymUser User { get; set; } = null!;
        public ICollection<PlaylistExcercise> Excercise { get; set; } = new List<PlaylistExcercise>();
    }
}
