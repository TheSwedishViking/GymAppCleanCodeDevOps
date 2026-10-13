namespace GymSwipe.ApplicationLayer.DTOs
{
    public class GymPlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateCreated { get; set; }
        public int UserId { get; set; }
        public List<PlaylistExerciseDTO> Excercise { get; set; } = new List<PlaylistExerciseDTO>();
        public bool HasBeenPlayedBefore { get; set; } = false;
        public int TimesPlayed { get; set; }
        public TimeSpan LatestElapsedTime { get; set; }
        public DateTime LastPlayed { get; set; }
        public double CaloriesBurnedFromLatestWorkout { get; set; }
    }
}
