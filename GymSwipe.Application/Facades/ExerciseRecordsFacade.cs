using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Controls;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.TimeDateFormat;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
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
        private readonly IRecordsValidator _recordsValidator;
        private readonly IPlaylistControls _playlistControls;
        private readonly IDateHandler _dateHandler;
        private PlaylistExerciseDTO _currentExercise;
        private WorkoutSession _session;
        private readonly LoggedInUser _user;
        private List<ExerciseRecordDTO> _addedRecords = new List<ExerciseRecordDTO>();

        public ExerciseRecordsFacade(
            IExerciseRecordsService exerciseRecordsService,
            IGymPlaylistService gymPlaylistService,
            IPlaylistControls playlistControls,
            IRecordsValidator recordsValidator,
            IDateHandler dateHandler,
            LoggedInUser loggedInUser,
            WorkoutSession workoutSession )
        {
            _exerciseRecordsService = exerciseRecordsService;
            _recordsValidator = recordsValidator;
            _playlistControls = playlistControls;
            _dateHandler = dateHandler;
            _session = workoutSession;
            _user = loggedInUser;
        }

        public async Task<List<string>> AddRecord(ExerciseRecordDTO currentRecord)
        {
            var errors = new List<string>();
            if (currentRecord == null)
            {
                errors.Add("Argument null");
                return errors;
            }
            errors =  _recordsValidator.Validate(currentRecord);
            if(errors.Count > 0)
            {
                errors.Add("Can't record exercise record, validate input");
                return errors;
            }
            currentRecord.UserId = _user.CurrentUser.Id;
            currentRecord.DateRecorded = _dateHandler.SetDateAsDateTime();
            _addedRecords.Add(currentRecord);
            return errors;
        }

        public async Task<bool> GetActiveStatus()
        {
            return  _session.IsActive;
        }

        public async Task<PlaylistExerciseDTO> GetFirstExercise(ObservableCollection<PlaylistExerciseDTO> excercises)
        {
            _currentExercise = await _playlistControls.GetFirstExercise(_session.CurrentPlaylist.Excercise);
            return _currentExercise;
        }

        public async Task<PlaylistExerciseDTO> GetNextExercise(PlaylistExerciseDTO current)
        {
            _currentExercise = await _playlistControls.GetNextExercise(current, _session.CurrentPlaylist.Excercise);
            if(current == _currentExercise)
            {

            }
            return _currentExercise;
        }
        public async Task<PlaylistExerciseDTO> GetPreviousExercise(PlaylistExerciseDTO ex)
        {
            _currentExercise = await _playlistControls.GetNextExercise(ex, _session.CurrentPlaylist.Excercise);
            if (ex == _currentExercise)
            {
                return null;
            }
            return _currentExercise;
        }

        public async Task<List<PlaylistExerciseDTO>> GetPlaylistExercises()
        {
            if(_session.CurrentPlaylist==null || _session.CurrentPlaylist.Excercise == null)
            {
                throw new Exception("No exercies found!");
            }
            return  _session.CurrentPlaylist.Excercise.ToList();
        }
        public async Task<List<ExerciseRecordDTO>> GetActiveRecords()
        {
            return  _addedRecords;
        }

        public async Task SaveRecords(List<ExerciseRecordDTO> records)
        {
            if(records.Count() > 0)
            {
                try
                {
                    await _exerciseRecordsService.SaveRecordsFromCurrentPlaylist(records);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

       
    }
}
