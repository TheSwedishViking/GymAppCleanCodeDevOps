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
            if(targetAreaDTO == null)
            {
                throw new ArgumentNullException("New target area invalid");
            }

            var existing = await _traningAreaService.GetTargetAreaByName(targetAreaDTO.Name);
            if(existing == null)
            {
                await _traningAreaService.AddNewArea(targetAreaDTO);
            }
            else
            {
                throw new Exception("Already exists");
            }
        }

        public async Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync()
        {
            return await _traningAreaService.GetExerciseTargetsAsync();
        }
    }
}
