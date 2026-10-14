using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Controls;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System.Diagnostics;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class WorkoutFacade : IWorkoutFacade
    {
        private readonly IUserService _userService;
        private readonly IGymPlaylistService _playlistService;
        private readonly IPlaylistControls _playlistControls;
        private readonly ICalorieCountingService _calorieCountingService;
        private readonly ITimeSetter _timeSetter;
        private readonly ITraningAreaService _traningAreaService;

        private readonly LoggedInUser _loggedInUser;
        private readonly WorkoutSession _workoutSession;
        private GymPlaylistDTO _playlist;
        private PlaylistExerciseDTO _currentExercise;
        private Stopwatch stopwatch;
        public WorkoutFacade(
            IExerciseService exerciseService,
            IUserService userService,
            ICalorieCountingService calorieCountingService,
            ITimeSetter timeSetter,
            IPlaylistControls playlistControls,
            IGymPlaylistService gymPlaylistService,
            ITraningAreaService traningAreaService,
            LoggedInUser loggedInUser,
            WorkoutSession session)
        {
            _playlistService = gymPlaylistService;
            _playlistControls = playlistControls; 
            _calorieCountingService = calorieCountingService;
            _traningAreaService = traningAreaService;
            _userService = userService;
            _loggedInUser = loggedInUser;
            _timeSetter = timeSetter;
            _workoutSession = session;
            stopwatch = new Stopwatch();

        }
        public async Task<GymPlaylistDTO> GetPlaylistByUserId(int userId)
        {
            var userValid = await _userService.GetUserById(userId);
            if (userValid == null)
            {
                throw new Exception("No user found");
            }

            var userHasPlaylists = await _playlistService.GetPlaylistsByUserId(userId);
            if (userHasPlaylists == null)
            {
                throw new Exception("No playlist found; point user towards creating a playlist, alt open swipe automatically");
            }

            _playlist = await _playlistService.GetTodaysPlaylist(userId);
            _workoutSession.Start(_playlist);
            return _playlist;
        }

        public async Task<PlaylistExerciseDTO> StartWorkout()
        {
            if (_playlist == null || _playlist.Excercise == null) return null;
            _currentExercise = _playlist.Excercise.FirstOrDefault();
            _workoutSession.Start(_playlist);
            stopwatch.Start();
            return _currentExercise;
        }
        public async Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current)
        {
            _currentExercise = await _playlistControls.GetNextExercise(current, _playlist.Excercise);
            if (_currentExercise == current)
            {
                return null;
            }

            return _currentExercise;
        }
        public async Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO current)
        {
            _currentExercise = await _playlistControls.GetPreviousExercise(current, _playlist.Excercise);
            if (_currentExercise == null)
            {
                throw new Exception("Null exercise");
            }
            return _currentExercise;
        }

        public async Task PauseWorkout()
        {
            stopwatch.Stop();
        }
    
        public async Task<GymPlaylistDTO> GetPlaylistById(int id)
        {

            var playlist = await _playlistService.GetPlaylist(id);
            if (playlist == null)
            {
                throw new KeyNotFoundException($"No playlist found, with provided Id: {id}");
            }
            _playlist = playlist;
            return _playlist;
        }

        public async Task CompleteWorkout()
        {
            stopwatch.Stop();

            var elapsedTime = _timeSetter.SetTime(stopwatch);

            var calories =  _calorieCountingService.CalculateCalories(
                _playlist.LatestElapsedTime
                ,_loggedInUser.CurrentUser.WeightKg,
                null);

             await _playlistService.RecordedCompletedWorkout(_playlist.Id, elapsedTime, calories);
        }
        public async Task<string> GetApproptiateImageForExercise(int exerciseId)
        {

            string img = await _traningAreaService.GetTargetAreaImage(exerciseId);

            return img + "_card.jpg";


        }
    }
}
