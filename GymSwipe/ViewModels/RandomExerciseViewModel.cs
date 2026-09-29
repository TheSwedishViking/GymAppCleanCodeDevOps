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
        public ICommand GetRandomExcerises { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public RandomExerciseViewModel(IExerciseFacade exerciseFacade)
        {
            _exerciseFacade = exerciseFacade;
            GetRandomExcerises = new Command(async () => await GetObserverableCollectionOfExcerises());
        }
        //Redneck way of clearing and adding to a observable collcetion
        public async Task GetObserverableCollectionOfExcerises()
        {
            if (ExerciseDTOs.Count != 0) {
                ExerciseDTOs.Clear();
            }
            var exs = await _exerciseFacade.GetRandomExercisesAsync();
            foreach(var ex in exs)
            {
                ExerciseDTOs.Add(ex);
            }
        }
    }
}
