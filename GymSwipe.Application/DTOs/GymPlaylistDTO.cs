namespace GymSwipe.ApplicationLayer.DTOs
{
    public class GymPlaylistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateCreated { get; set; }
        public int UserId { get; set; }
        public List<PlaylistExerciseDTO> Excercise { get; set; } = new List<PlaylistExerciseDTO>();
    }
}
