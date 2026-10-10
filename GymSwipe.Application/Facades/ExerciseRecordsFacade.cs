using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class ExerciseRecordsFacade:IExerciseRecordsFacade
    {
        private readonly IExerciseRecordsService _exerciseRecordsService;
        private readonly IGymPlaylistService _playlistService;
        private PlaylistExcercise _currentExercise;
        private WorkoutSession _session;
        private readonly LoggedInUser _user;
        private List<ExerciseRecordDTO> _addedRecords = new List<ExerciseRecordDTO>();

        public ExerciseRecordsFacade(
            IExerciseRecordsService exerciseRecordsService,
            IGymPlaylistService gymPlaylistService,
            LoggedInUser loggedInUser,
            WorkoutSession workoutSession )
        {
            _exerciseRecordsService = exerciseRecordsService;
            _playlistService = gymPlaylistService;
            _session = workoutSession;
            _user = loggedInUser;
        }

        public async Task AddRecord(ExerciseRecordDTO currentRecord)
        {
            if (currentRecord == null)
            {
                throw new ArgumentNullException(nameof(currentRecord));
            }
            currentRecord.UserId = _user.CurrentUser.Id;
            _addedRecords.Add(currentRecord);
        }

        public async Task<bool> GetActiveStatus()
        {
            return  _session.IsActive;
        }

        public async Task<PlaylistExcercise> GetFirstExercise(ObservableCollection<PlaylistExcercise> excercises)
        {
            _currentExercise = await _playlistService.GetFirstExercise(_session.CurrentPlaylist.Excercise);
            return _currentExercise;
        }

        public async Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current)
        {
            _currentExercise = await _playlistService.GetNextExercise(current, _session.CurrentPlaylist.Excercise);
            if(current == _currentExercise)
            {
                throw new Exception("NUll exercise to go back to ");
            }
            return _currentExercise;
        }
        public async Task<PlaylistExcercise> GetPreviousExercise(PlaylistExcercise ex)
        {
            _currentExercise = await _playlistService.GetNextExercise(ex, _session.CurrentPlaylist.Excercise);
            if (ex == _currentExercise)
            {
                return null;
            }
            return _currentExercise;
        }

        public async Task<List<PlaylistExcercise>> GetPlaylistExercises()
        {
            if(_session.CurrentPlaylist==null || _session.CurrentPlaylist.Excercise == null)
            {
                throw new Exception("No exercies found!");
            }
            return  _session.CurrentPlaylist.Excercise.ToList();
        }


        public Task<bool> SaveRecords(List<ExerciseRecordDTO> records)
        {
            throw new NotImplementedException();
        }

       
    }
}
