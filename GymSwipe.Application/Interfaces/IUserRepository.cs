using GymSwipe.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(GymUserDTO dto);
        Task<GymUserDTO> GetUserById(int id);

    }
}
