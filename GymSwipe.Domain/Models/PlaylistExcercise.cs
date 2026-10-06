namespace GymSwipe.Domain.Models
{
    public class PlaylistExcercise
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; } = null!;   // add
        public int PlaylistId { get; set; }
        public GymPlaylist Playlist { get; set; } = null!; // add
        public int PlaylistOrder { get; set; }
    }
}
