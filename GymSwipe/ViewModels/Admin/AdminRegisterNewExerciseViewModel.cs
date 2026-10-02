using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels.Admin
{
    public class AdminRegisterNewExerciseViewModel : INotifyPropertyChanged
    {
        public Exercise NewExercise { get; set; } = new Exercise();

        public ObservableCollection<ExerciseTargetArea> Available = new ObservableCollection<ExerciseTargetArea>();
        public ObservableCollection<ExerciseTargetArea> Selected = new ObservableCollection<ExerciseTargetArea>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand RegisterNewExerciseCommand { get; }
        public ICommand AddTargetArea { get; }
        public ICommand RemoveTargetArea { get; }

        public AdminRegisterNewExerciseViewModel()
        {
            RegisterNewExerciseCommand = new Command(async () => await TryRegisterNewExercise());
        }

        private async Task TryRegisterNewExercise()
        {
            Console.WriteLine(NewExercise);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
