using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class PlaylistExecericseService : IPlaylistExecericseService
    {
        private readonly IGymPlaylistRepository _playlistRepo;
        private readonly IExerciseRepository _exerciseRepository;
        public PlaylistExecericseService(IGymPlaylistRepository playlistrepo, IExerciseRepository exerciseRepository)
        {
            _playlistRepo = playlistrepo;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<List<PlaylistExcercise>> GetExercisesByPlaylistIdAsync(int playlistId)
        {
            return null;
           // return await _repo.GetExercisesByPlaylistIdAsync(playlistId);
        }

        public async Task SavePlayListToUser(List<ExerciseDTO> addedExercises, GymUser currentUser)
        {
            GymPlaylist newPlaylist = new GymPlaylist();
            List<PlaylistExcercise> domain = new List<PlaylistExcercise>();

            for (int i = 0; i < addedExercises.Count; i++)
            {
                {
                    newPlaylist.Excercise.Add(ConvertToDomain(addedExercises[i], i));
                }
            }
            newPlaylist.Name = currentUser.Firstname + " playlist" +  DateTime.Now.ToString("M");
            newPlaylist.UserId = currentUser.Id;
            newPlaylist.DateCreated = new DateOnly(2001, 02, 11);

            await _playlistRepo.SavePlayList(newPlaylist);

        }
        private PlaylistExcercise ConvertToDomain(ExerciseDTO dTO, int order)
        {
            return new PlaylistExcercise
            {
                ExerciseId = dTO.Id,

                PlaylistOrder = order,
            };
            
        }
    }
}
