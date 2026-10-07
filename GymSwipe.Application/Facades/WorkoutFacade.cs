using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class WorkoutFacade : IWorkoutFacade
    {
        private readonly IExerciseService _exerciseService;
        private readonly IUserService _userService;
        private readonly IGymPlaylistService _playlistService;
        private GymPlaylist _playlist;
        public WorkoutFacade(IExerciseService exerciseService, IUserService userService, IGymPlaylistService gymPlaylistService)
        {
            _exerciseService = exerciseService;
            _playlistService = gymPlaylistService;
            _userService = userService;
        }
        public async Task GetPlaylist(int userId)
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
            throw new NotImplementedException();
        }

        public Task QuitWorkout()
        {
            throw new NotImplementedException();
        }

        public Task RecordUserWorkout()
        {
            throw new NotImplementedException();
        }

        public Task StartWorkout()
        {
            throw new NotImplementedException();
        }
    }
}
