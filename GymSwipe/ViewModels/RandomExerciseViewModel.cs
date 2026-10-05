using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels
{
    public class RandomExerciseViewModel : INotifyPropertyChanged
    {
        private IExerciseFacade _exerciseFacade;
        //ObservableCollection => MAUI list
        private ObservableCollection<ExerciseDTO> _exerciseDTOs = new();
        public ObservableCollection<ExerciseDTO> ExerciseDTOs
        {
            get {  return _exerciseDTOs; }
            set
            {
                _exerciseDTOs = value;
                OnPropertyChanged(nameof(ExerciseDTOs));
            }
        }
        public ICommand GetAllExercises { get; }
        public ICommand GetRandomExcerises { get; }
        public ICommand GetChestExercises { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RandomExerciseViewModel(IExerciseFacade exerciseFacade)
        {
            _exerciseFacade = exerciseFacade;
            GetAllExercises = new Command(async () => await GetObserverableCollectionOfExcerises());
            GetRandomExcerises = new Command(async () => await GetRandomCollectionOfExercises());
            GetChestExercises = new Command(async () => await GetChestExercisesCollection());
        }
        private async Task ClearList()
        {
            if (ExerciseDTOs.Count != 0)
            {
                ExerciseDTOs.Clear();
            }
        }
        public async Task GetRandomCollectionOfExercises()
        {
            await ClearList();
            var random = await _exerciseFacade.GetRandomExercisesAsync();
            foreach (var rand in random)
            {
                ExerciseDTOs.Add(rand);
            }
        }
        public async Task GetChestExercisesCollection()
        {
            await ClearList();
            var chests = await _exerciseFacade.GetChestExerciesAsync(1);
            foreach(var chest in chests)
            {
                ExerciseDTOs.Add(chest);
            }
        }
        //Redneck way of clearing and adding to a observable collcetion
        public async Task GetObserverableCollectionOfExcerises()
        {
            await ClearList();
            var exs = await _exerciseFacade.GetAllExercisesAsync();
            foreach(var ex in exs)
            {
                ExerciseDTOs.Add(ex);
            }
        }
    }
}
