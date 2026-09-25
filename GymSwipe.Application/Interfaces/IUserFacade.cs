using GymSwipe.Application.DTOs;
using GymSwipe.Application.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IUserFacade
    {
        Task<GymUserDTO> GetGymUserDTOAsync(int  id);
        Task<GymUserDTO> CreateGymUser(RequestCreateGymUserDTO request);
    }
}
