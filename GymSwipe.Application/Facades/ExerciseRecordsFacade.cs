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

        public ExerciseRecordsFacade(IExerciseRecordsService exerciseRecordsService, WorkoutSession workoutSession)
        {
            _exerciseRecordsService = exerciseRecordsService;
            _session = workoutSession;
        }

        public async Task<bool> GetActiveStatus()
        {
            return  _session.IsActive;
        }

        public async Task<PlaylistExcercise> GetNextExercise(PlaylistExcercise current)
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

        public Task<bool> SaveRecords(List<ExerciseRecords> records)
        {
            throw new NotImplementedException();
        }
    }
}
