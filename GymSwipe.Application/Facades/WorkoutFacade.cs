using GymSwipe.ApplicationLayer.Interfaces;
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
        private GymPlaylist _playlist;
        private PlaylistExcercise _currentExercise;
        private Stopwatch stopwatch;
        public WorkoutFacade(IExerciseService exerciseService, IUserService userService, IGymPlaylistService gymPlaylistService)
        {
            _exerciseService = exerciseService;
            _playlistService = gymPlaylistService;
            _userService = userService;
            stopwatch = new Stopwatch();
        }
        public async Task<GymPlaylist> GetPlaylist(int userId)
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

            return _playlist;
        }

        public async Task<PlaylistExcercise> StartWorkout()
        {
            if (_playlist == null || _playlist.Excercise == null) return null;
            _currentExercise = _playlist.Excercise.FirstOrDefault();
            stopwatch.Start();
            return _currentExercise;
        }
        public async Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current)
        {
            var list = _playlist.Excercise.ToList();
            var index = list.FindIndex(e=>e.Id== current.Id);
            if(index < 0)
            {
                return _currentExercise;
            }
            if(index+1>= list.Count)
            {
                return null;
            }
            _currentExercise = list[index + 1];
            return _currentExercise;
        }
        public async Task<PlaylistExcercise> GetPreviousExercise(PlaylistExcercise current)
        {
            
            var list = _playlist.Excercise.ToList();
            var index = list.FindIndex(e => e.Id == current.Id);
            if (index -1<0)
            {
                return _currentExercise;
            }
            _currentExercise = list[index - 1];
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
            return true;
        }

       

        
    }
}
