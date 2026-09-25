using GymSwipe.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Interfaces
{
    public interface IUserService
    {
        Task<GymUserDTO> GetUserById(string id);
        Task<GymUserDTO> GetUserByEmail(string email);
        Task<string> GenerateFriendCode();
    }
}
