using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class ExerciseRecordsFacade:IExerciseRecordsFacade
    {
        private readonly IExerciseRecordsService _exerciseRecordsService;
        private WorkoutSession _session;
        private List<ExerciseRecordDTO> _addedRecords = new List<ExerciseRecordDTO>();

        public ExerciseRecordsFacade(IExerciseRecordsService exerciseRecordsService, WorkoutSession workoutSession)
        {
            _exerciseRecordsService = exerciseRecordsService;
            _session = workoutSession;
        }

        public async Task AddRecord(ExerciseRecordDTO currentRecord)
        {
            if (currentRecord == null) return;
            _addedRecords.Add(currentRecord);
        }

        public async Task<bool> GetActiveStatus()
        {
            return  _session.IsActive;
        }

        public async Task<PlaylistExcercise> GetFirstExercise()
        {
            return _session.CurrentPlaylist.Excercise.FirstOrDefault();
        }

        public async Task<PlaylistExcercise> GetNextExercise(ExerciseDTO current)
        {
            throw new NotImplementedException();
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
