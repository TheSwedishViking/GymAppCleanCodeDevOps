using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Facades;
using GymSwipe.ApplicationLayer.Interfaces;
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

        public ObservableCollection<TargetAreaDTO> Available { get; } = new ObservableCollection<TargetAreaDTO>();
        public ObservableCollection<TargetAreaDTO> Selected = new ObservableCollection<TargetAreaDTO>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private IAdminAddExerciseFacade _adminAddExerciseFacade;

        public ICommand RegisterNewExerciseCommand { get; }
        public ICommand AddTargetArea { get; }
        public ICommand RemoveTargetArea { get; }

        public AdminRegisterNewExerciseViewModel(IAdminAddExerciseFacade adminAddExerciseFacade )
        {
            _adminAddExerciseFacade = adminAddExerciseFacade;
            RegisterNewExerciseCommand = new Command(async () => await TryRegisterNewExercise());
        }
        public async Task OnLoadPageGetExerciseAreas()
        {
            var available = await _adminAddExerciseFacade.GetExerciseTargetsAsync();
            foreach(var  target in available)
            {
                Available.Add(target);
            }
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
