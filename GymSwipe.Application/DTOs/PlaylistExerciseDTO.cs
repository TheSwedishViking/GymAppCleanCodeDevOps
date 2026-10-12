namespace GymSwipe.ApplicationLayer.DTOs
{
    public class PlaylistExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VideoSourceLink { get; set; } = string.Empty;
        public int PlaylistOrder { get; set; }
    }
}
