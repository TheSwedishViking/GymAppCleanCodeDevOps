using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Display;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
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
        private readonly IDisplayInfoHelper _playInfoHelper;
        public AdminRegisterNewTrainingAreaViewModel(IAdminAddTrainingAreaFacade adminAddTrainingAreaFacade, IDisplayInfoHelper playInfoHelper)
        {
            _adminAddTrainingAreaFacade = adminAddTrainingAreaFacade;
            AddNewTargetAreaCommand = new Command(async () => await TryAddNewExerciseTargetArea());
            _playInfoHelper = playInfoHelper;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public async Task TryAddNewExerciseTargetArea()
        {
            try
            {
                var errors = await _adminAddTrainingAreaFacade.AddTargetArea(NewTargetArea);
                if(errors.Count > 0)
                {
                    await _playInfoHelper.DisplayErrorAlert(errors);
                    return;
                }
                await Task.Yield();
                await RefreshList();
                return;
            }
            catch (Exception ex)
            {
                await _playInfoHelper.DisplayStandardAlert(ex.Message);

            }
        }
        public async Task GetAreas()
        {
            await RefreshList();
         
        }
        public async Task RefreshList()
        {
            Current.Clear();
            var areas = await _adminAddTrainingAreaFacade.GetTargetAreaDTOsAsync();
            foreach (var a in areas)
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
