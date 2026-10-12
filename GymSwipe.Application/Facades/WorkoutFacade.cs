using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class WorkoutFacade : IWorkoutFacade
    {
        private readonly IExerciseService _exerciseService;
        private readonly IUserService _userService;
        private readonly IGymPlaylistService _playlistService;
        private readonly ICalorieCountingService _calorieCountingService;
        private readonly LoggedInUser _loggedInUser;
        private readonly WorkoutSession _workoutSession;
        private GymUser _user;
        private GymPlaylist _playlist;
        private PlaylistExercise _currentExercise;
        private Stopwatch stopwatch;
        public WorkoutFacade(
            IExerciseService exerciseService, 
            IUserService userService, 
            ICalorieCountingService calorieCountingService,
            IGymPlaylistService gymPlaylistService,
            LoggedInUser loggedInUser,
            WorkoutSession session)
        {
            _exerciseService = exerciseService;
            _playlistService = gymPlaylistService;
            _calorieCountingService = calorieCountingService;
            _userService = userService;
            _loggedInUser = loggedInUser;
            _workoutSession = session;
            stopwatch = new Stopwatch();

        }
        public async Task<GymPlaylist> GetPlaylistByUserId(int userId)
        {
            var userValid = await _userService.GetUserById(userId);
            if (userValid == null)
            {
                throw new Exception("No user found");
            }

            var userHasPlaylists = await _playlistService.GetPlaylistsByUserId(userId);
            if(userHasPlaylists == null)
            {
                throw new Exception("No playlist found; point user towards creating a playlist, alt open swipe automatically");
            }

           _playlist = await _playlistService.GetTodaysPlaylist(userId);
            _workoutSession.Start(_playlist);
            return _playlist;
        }

        public async Task<PlaylistExercise> StartWorkout()
        {
            if (_playlist == null || _playlist.Exercises == null) return null;
            _currentExercise = _playlist.Exercises.FirstOrDefault();
            _workoutSession.Start(_playlist);
            stopwatch.Start();
            return _currentExercise;
        }
        public async Task<PlaylistExercise> GetNextExercise(PlaylistExercise current)
        {
            _currentExercise = await _playlistService.GetNextExercise(current, _playlist.Exercises);
            if(_currentExercise == current)
            {
                return null;
            }
            return _currentExercise;
        }
        public async Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise current)
        {
            _currentExercise = await _playlistService.GetPreviousExercise(current, _playlist.Exercises);
            if(_currentExercise == null)
            {
                throw new Exception("Null exercise");
            }
            return _currentExercise;
        }

        public async Task PauseWorkout()
        {
            stopwatch.Stop();
        }
        public async Task RecordUserWorkout()
        {
            //See over models to handle; 
            //A playlist as completed
            //A record of performed sets
           
        }
        public async Task<bool> QuitWorkout()
        {
            _workoutSession.Clear();
            return true;
        }

        public async Task<GymPlaylist> GetPlaylistById(int id)
        {
            try
            {
                var playlist = await _playlistService.GetPlaylist(id);
                if (playlist == null)
                {
                    throw new ArgumentNullException("No playlist found, but we have a provided Id?");
                }
                _playlist = playlist;
                return _playlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new GymPlaylist();
        }

        public async Task CompleteWorkout()
        {
             //_workoutSession.Clear();
            _playlist.HasBeenPlayedBefore = true;
            _playlist.LatestElapsedTime = await _playlistService.SetTime(stopwatch);
            _playlist.LastPlayed = await _playlistService.SetDate();
            _playlist.TimesPlayed = await _playlistService.IncrimentTimesPlayed(_playlist.TimesPlayed);

            _playlist.CaloriesBurnedFromLatestWorkout = await _calorieCountingService.CalculateCalories(_playlist.LatestElapsedTime, _user.WeightKg, 5);

            await _playlistService.UpdatePlaylistInfo(_playlist);
        }
    }
}
