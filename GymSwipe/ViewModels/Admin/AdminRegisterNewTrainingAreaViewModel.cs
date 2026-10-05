using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ViewModels.Admin
{
    public class AdminRegisterNewTrainingAreaViewModel : INotifyPropertyChanged
    {
        public TargetAreaDTO NewTargetArea { get; set; } = new TargetAreaDTO();
        public ObservableCollection<TargetAreaDTO> Current { get; } = new ObservableCollection<TargetAreaDTO>();
        public ICommand GetAreasCommand { get; }
        public ICommand AddNewTargetAreaCommand { get; }

        private readonly IAdminAddTrainingAreaFacade _adminAddTrainingAreaFacade;
        public AdminRegisterNewTrainingAreaViewModel(IAdminAddTrainingAreaFacade adminAddTrainingAreaFacade)
        {
            _adminAddTrainingAreaFacade = adminAddTrainingAreaFacade;
            AddNewTargetAreaCommand = new Command(async () => await TryAddNewExerciseTargetArea());

        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task TryAddNewExerciseTargetArea()
        {
            await _adminAddTrainingAreaFacade.AddTargetArea(NewTargetArea);
        }
        public async Task GetAreas()
        {
            Current.Clear();
            var areas = await _adminAddTrainingAreaFacade.GetTargetAreaDTOsAsync();
            foreach(var a in areas)
            {
                Current.Add(a);
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
