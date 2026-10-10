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
using System.Windows.Markup;

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
                CurrentRecord = value is null ? null : new ExerciseRecordDTO { ExerciseId = value.ExerciseId };
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
        public ICommand PreviousExerciseCommand { get; }
        private readonly IExerciseRecordsFacade _exerciseRecordsFacade;
        public ExerciseEvaulationViewModel(IExerciseRecordsFacade exerciseRecordsFacade)
        {
            _exerciseRecordsFacade = exerciseRecordsFacade;
            NextExerciseToEvaluateCommand = new Command<ExerciseRecordDTO>(async e => await RegisterRecord(e));
            PreviousExerciseCommand = new Command(async e => await PreviousRecord());

            CurrentRecord = new ExerciseRecordDTO();
        }
        public async Task RegisterRecord(ExerciseRecordDTO record)
        {
            await _exerciseRecordsFacade.AddRecord(record);
            await ResetRecordBinding();
            await NextExercise(CurrentExercise);
        }
        public async Task PreviousRecord()
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
            CurrentExercise = await _exerciseRecordsFacade.GetFirstExercise(Excercises);
            //CurrentExercise = await _exerciseRecordsFacade.GetNextExercise(CurrentExercise);
            if(CurrentExercise == null)
            {
                Console.WriteLine("Done!");
            }
        }
        public async Task NextExercise(PlaylistExcercise ex)
        {
             await _exerciseRecordsFacade.AddRecord(CurrentRecord);
            await ResetRecordBinding();
            CurrentExercise = await _exerciseRecordsFacade.GetNextExercise(ex);
            if(CurrentExercise == null)
            {
                await Shell.Current.Navigation.PopToRootAsync();
            }
        }
        public async Task PreviousExercise(PlaylistExcercise ex)
        {
            //await _exerciseRecordsFacade.AddRecord(CurrentRecord);
            await ResetRecordBinding();
            CurrentExercise = await _exerciseRecordsFacade.GetPreviousExercise(ex);
        }

        private async Task ResetRecordBinding()
        {
            CurrentRecord = new ExerciseRecordDTO
            {
                ExerciseId = CurrentExercise.ExerciseId 
            };
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
