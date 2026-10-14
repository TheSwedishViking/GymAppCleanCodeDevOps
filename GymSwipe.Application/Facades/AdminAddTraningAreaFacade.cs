using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class AdminAddTraningAreaFacade : IAdminAddTrainingAreaFacade
    {
        private readonly ITraningAreaService _traningAreaService;
        public AdminAddTraningAreaFacade(ITraningAreaService traningAreaService)
        {
            _traningAreaService = traningAreaService;
        }
        public async Task AddTargetArea(TargetAreaDTO targetAreaDTO)
        {
            //Real basic, somehow null, throw
            ArgumentNullException.ThrowIfNull(targetAreaDTO);

            var existing = await _traningAreaService.GetTargetAreaByName(targetAreaDTO.Name);
            if(existing != null)
            {
                throw new InvalidOperationException($"Target area '{targetAreaDTO.Name}' already exists.");
            }
            await _traningAreaService.AddNewArea(targetAreaDTO);
        }

        public async Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync()
        {
            var targets = await _traningAreaService.GetExerciseTargetsAsync();
            if(targets == null)
            {
                throw new ArgumentNullException("Retireved no target areas!");
            }
            return await _traningAreaService.GetExerciseTargetsAsync();
        }
    }
}
