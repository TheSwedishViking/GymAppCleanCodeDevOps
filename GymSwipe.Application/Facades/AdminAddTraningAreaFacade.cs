using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Facades;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.ApplicationLayer.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace GymSwipe.ApplicationLayer.Facades
{
    public class AdminAddTraningAreaFacade : IAdminAddTrainingAreaFacade
    {
        private readonly ITraningAreaService _traningAreaService;
        private readonly IAdminCreatorValidator _adminCreatorValidator;
        public AdminAddTraningAreaFacade(ITraningAreaService traningAreaService, IAdminCreatorValidator adminCreatorValidator)
        {
            _traningAreaService = traningAreaService;
            _adminCreatorValidator = adminCreatorValidator;
        }
        public async Task<List<string>> AddTargetArea(TargetAreaDTO targetAreaDTO)
        {
            List<string> errors = new List<string>();

            var existing = await _traningAreaService.GetTargetAreaByName(targetAreaDTO.Name);
            if(existing != null)
            {
                errors.Add("Exercise already exists!");
                return errors;
            }
            errors = await _adminCreatorValidator.ValidateNewTargetArea(targetAreaDTO);
            if(errors.Count > 0)
            {
                return errors;
            }
            await _traningAreaService.AddNewArea(targetAreaDTO);
            return errors;
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
