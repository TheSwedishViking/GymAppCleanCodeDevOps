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
        public ExerciseDTO NewExercise { get; set; } = new ExerciseDTO();

        public TargetAreaDTO TargetAreaTapped { get; set; }
        public ObservableCollection<TargetAreaDTO> Available { get; } = new ObservableCollection<TargetAreaDTO>();
        public ObservableCollection<TargetAreaDTO> Selected { get;  } = new ObservableCollection<TargetAreaDTO>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private IAdminAddExerciseFacade _adminAddExerciseFacade;

        public ICommand RegisterNewExerciseCommand { get; }
        public ICommand AddTargetAreaCommand { get; }
        public ICommand RemoveTargetAreaCommand { get; }

        public AdminRegisterNewExerciseViewModel(IAdminAddExerciseFacade adminAddExerciseFacade )
        {
            _adminAddExerciseFacade = adminAddExerciseFacade;
            AddTargetAreaCommand = new Command<TargetAreaDTO>(async area => await AddAreaToExercise(area));
            RemoveTargetAreaCommand = new Command<TargetAreaDTO>(async area => await RemoveAreaToExercise(area));
            RegisterNewExerciseCommand = new Command(async () => await TryRegisterNewExercise());
        }
        public async Task AddAreaToExercise(TargetAreaDTO target)
        {
            Available.Remove(target);
            Selected.Add(target);
        }
        public async Task RemoveAreaToExercise(TargetAreaDTO target)
        {
            Available.Add(target);
            Selected.Remove(target);
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
            if(Selected.Count == 0)
            {
                return;
            }
            NewExercise.TargetAreaNames = Selected.Select(e=>e.Name).ToList();
            Console.WriteLine(NewExercise);
            await _adminAddExerciseFacade.AddExercise(NewExercise);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
