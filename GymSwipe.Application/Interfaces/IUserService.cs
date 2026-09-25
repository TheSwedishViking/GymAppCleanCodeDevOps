using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserService
    {
        Task<GymUserDTO?> TryAndCreateUserThroughRequestModelAsync(RequestCreateGymUserDTO request);
        Task<GymUserDTO> GetUserById(string id);
        Task<GymUserDTO> GetUserByEmail(string email);
        Task<string> GenerateFriendCode();
    }
}
