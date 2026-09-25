using GymSwipe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<GymUserDTO> GetUserById(int id);

    }
}
