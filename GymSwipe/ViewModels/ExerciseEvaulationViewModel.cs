using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace GymSwipe.ViewModels
{
    //Load specific playlist id to evaluate each exercise to keep records for later
    public class ExerciseEvaulationViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private int? _playListId;

        private PlaylistExcercise _currentExerciseToRecord;
        public PlaylistExcercise CurrentExercise
        {
            get => _currentExerciseToRecord;
            set
            {
                if(_currentExerciseToRecord ==  value) return;
                _currentExerciseToRecord = value;
                OnPropertyChanged(nameof(CurrentExercise));
                OnPropertyChanged(nameof(CurrentRecord));

            }
        }
        private ExerciseRecords _currentRecord;
        public ExerciseRecords CurrentRecord
        {
            get { return _currentRecord; }
            set {
                if(_currentRecord == value) return;
                _currentRecord = value;
                OnPropertyChanged(nameof(CurrentRecord));
            
            }
        }
        public ObservableCollection<PlaylistExcercise> Excercises { get; set; } = new ObservableCollection<PlaylistExcercise>();


        private readonly IExerciseRecordsFacade _exerciseRecordsFacade;
        public ExerciseEvaulationViewModel(IExerciseRecordsFacade exerciseRecordsFacade)
        {
            _exerciseRecordsFacade = exerciseRecordsFacade;
        }
        public async Task InitalizeAsync()
        {
            if(_playListId == null)
            {
                throw new Exception("No id?");
            }
            if (!await _exerciseRecordsFacade.GetActiveStatus())
            {
                throw new Exception("Nothing to evaluate? How'd we get here?");
            }
            await LoadExercises();
        }
        public async Task LoadExercises()
        {
            Excercises.Clear();
            Excercises = new ObservableCollection<PlaylistExcercise>(await _exerciseRecordsFacade.GetPlaylistExercises());
        }
        public async Task NextExercise(PlaylistExcercise ex)
        {
            CurrentExercise = await _exerciseRecordsFacade.GetNextExercise(ex);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _playListId = query.TryGetValue("PlayListId", out var value) && value is int id ? id : null;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
