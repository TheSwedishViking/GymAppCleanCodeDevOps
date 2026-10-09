using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.ApplicationLayer.Services.SessionServices;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

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
        private ExerciseRecordDTO _currentRecord;
        public ExerciseRecordDTO CurrentRecord
        {
            get { return _currentRecord; }
            set {
                if(_currentRecord == value) return;
                _currentRecord = value;
                OnPropertyChanged(nameof(CurrentRecord));
            
            }
        }
        public ObservableCollection<PlaylistExcercise> Excercises { get; set; } = new ObservableCollection<PlaylistExcercise>();

        public ICommand NextExerciseToEvaluateCommand { get; }

        private readonly IExerciseRecordsFacade _exerciseRecordsFacade;
        public ExerciseEvaulationViewModel(IExerciseRecordsFacade exerciseRecordsFacade)
        {
            _exerciseRecordsFacade = exerciseRecordsFacade;
            NextExerciseToEvaluateCommand = new Command<ExerciseRecordDTO>(async e => await RegisterRecord(e));
        }
        public async Task RegisterRecord(ExerciseRecordDTO record)
        {

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
            CurrentExercise = await _exerciseRecordsFacade.GetNextExercise();
        }
        public async Task NextExercise(ExerciseDTO ex)
        {
            await _exerciseRecordsFacade.AddRecord(CurrentRecord);
            await ResetRecordBinding(CurrentRecord);
            CurrentExercise = await _exerciseRecordsFacade.GetNextExercise(ex);
        }

        private async Task ResetRecordBinding(ExerciseRecordDTO currentRecord)
        {
            currentRecord.WeightKg = 0;
            currentRecord.Sets = 0;
            currentRecord.Repetitions = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _playListId = query.TryGetValue("PlaylistId", out var value) && value is int id ? id : null;
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
