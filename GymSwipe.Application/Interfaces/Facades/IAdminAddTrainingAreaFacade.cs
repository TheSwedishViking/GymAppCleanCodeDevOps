using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Facades
{
    public interface IAdminAddTrainingAreaFacade
    {
        Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync();
        Task<List<string>> AddTargetArea(TargetAreaDTO targetAreaDTO);
    }
}
