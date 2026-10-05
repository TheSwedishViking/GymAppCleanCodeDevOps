using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IAdminAddTrainingAreaFacade
    {
        Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync();
        Task AddTargetArea(TargetAreaDTO targetAreaDTO);
    }
}
