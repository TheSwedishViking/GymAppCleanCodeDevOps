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

        private PlaylistExerciseDTO _currentExerciseToRecord;
        public PlaylistExerciseDTO CurrentExercise
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
        public ObservableCollection<PlaylistExerciseDTO> Excercises { get; set; } = new ObservableCollection<PlaylistExerciseDTO>();

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
            try
            {
                var possibleErrors = await _exerciseRecordsFacade.AddRecord(record);
                if(possibleErrors.Count > 0)
                {
                    string completeMsg = string.Join("\n", possibleErrors);
                    await Shell.Current.DisplayAlertAsync("Validation error", completeMsg, "OK");
                }
                else
                {
                    await ResetRecordBinding();
                    await NextExercise(CurrentExercise);
                }
             
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Notice", ex.Message, "OK");
            }
        
        }
        public async Task PreviousRecord()
        {
            //Needed? Maybe?
        }
        public async Task InitalizeAsync()
        {
            if(_playListId == null)
            {
                await Shell.Current.DisplayAlertAsync("Notice", "Page was loaded with no playlist reference. Returning", "OK");
                await Shell.Current.Navigation.PopToRootAsync();
            }
            if (!await _exerciseRecordsFacade.GetActiveStatus())
            {
                await Shell.Current.DisplayAlertAsync("Notice", "No playlist in session, can't evaluate. Returning", "OK");
                await Shell.Current.Navigation.PopToRootAsync();

            }
            await LoadExercises();
        }
        public async Task LoadExercises()
        {
            Excercises.Clear();
            Excercises = new ObservableCollection<PlaylistExerciseDTO>(await _exerciseRecordsFacade.GetPlaylistExercises());
            CurrentExercise = await _exerciseRecordsFacade.GetFirstExercise(Excercises);
        }
        public async Task NextExercise(PlaylistExerciseDTO ex)
        {
             //await _exerciseRecordsFacade.AddRecord(CurrentRecord);
            CurrentExercise = await _exerciseRecordsFacade.GetNextExercise(ex);
            if(CurrentExercise == null)
            {
                await _exerciseRecordsFacade.SaveRecords(await _exerciseRecordsFacade.GetActiveRecords());
                await Task.Yield();
                await Shell.Current.Navigation.PopToRootAsync();
                return;
            }
        }
        public async Task PreviousExercise(PlaylistExerciseDTO ex)
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
