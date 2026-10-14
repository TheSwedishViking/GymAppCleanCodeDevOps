using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.ClientInterfaces
{
    public interface ITrainingAreasClient
    {
        Task<List<TargetAreaDTO>> GetTargetAreaDTOsAsync();
        Task<TargetAreaDTO> GetTargetAreaDTOById(int id);
    }
}
