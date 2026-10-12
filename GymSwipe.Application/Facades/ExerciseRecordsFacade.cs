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
        private readonly IDateHandler _dateHandler;
        private PlaylistExercise _currentExercise;
        private WorkoutSession _session;
        private readonly LoggedInUser _user;
        private List<ExerciseRecordDTO> _addedRecords = new List<ExerciseRecordDTO>();

        public ExerciseRecordsFacade(
            IExerciseRecordsService exerciseRecordsService,
            IGymPlaylistService gymPlaylistService,
            IDateHandler dateHandler,
            LoggedInUser loggedInUser,
            WorkoutSession workoutSession )
        {
            _exerciseRecordsService = exerciseRecordsService;
            _playlistService = gymPlaylistService;
            _dateHandler = dateHandler;
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
            currentRecord.DateRecorded = await _dateHandler.SetDateAsDateTime();
            _addedRecords.Add(currentRecord);
        }

        public async Task<bool> GetActiveStatus()
        {
            return  _session.IsActive;
        }

        public async Task<PlaylistExercise> GetFirstExercise(ObservableCollection<PlaylistExercise> excercises)
        {
            _currentExercise = await _playlistService.GetFirstExercise(_session.CurrentPlaylist.Exercises);
            return _currentExercise;
        }

        public async Task<PlaylistExercise> GetNextExercise(PlaylistExercise current)
        {
            _currentExercise = await _playlistService.GetNextExercise(current, _session.CurrentPlaylist.Exercises);
            if(current == _currentExercise)
            {
            }
            return _currentExercise;
        }
        public async Task<PlaylistExercise> GetPreviousExercise(PlaylistExercise ex)
        {
            _currentExercise = await _playlistService.GetNextExercise(ex, _session.CurrentPlaylist.Exercises);
            if (ex == _currentExercise)
            {
                return null;
            }
            return _currentExercise;
        }

        public async Task<List<PlaylistExercise>> GetPlaylistExercises()
        {
            if(_session.CurrentPlaylist==null || _session.CurrentPlaylist.Exercises == null)
            {
                throw new Exception("No exercies found!");
            }
            return  _session.CurrentPlaylist.Exercises.ToList();
        }
        public async Task<List<ExerciseRecordDTO>> GetActiveRecords()
        {
            return  _addedRecords;
        }

        public async Task<bool> SaveRecords(List<ExerciseRecordDTO> records)
        {
            if(records.Count() > 0)
            {
                try
                {
                    await _exerciseRecordsService.SaveRecordsFromCurrentPlaylist(records);
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }

       
    }
}
