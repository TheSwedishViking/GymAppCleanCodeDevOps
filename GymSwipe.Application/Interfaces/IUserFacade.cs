using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserFacade
    {
        Task<GymUserDTO> GetGymUserDTOAsync(int  id);
        Task<GymUserDTO> CreateGymUser(RequestCreateGymUserDTO request);
    }
}
