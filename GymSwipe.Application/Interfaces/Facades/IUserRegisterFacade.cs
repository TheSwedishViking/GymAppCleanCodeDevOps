using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces.Facades
{
    public interface IUserRegisterFacade
    {
        Task<List<string>> ValidateUser(RequestCreateGymUserDTO createGymUserDTO);
        Task<List<string>> ValidateEmail(string email);
        Task<List<string>> CheckIfEmailIsUnique(string email);
        Task<GymUserDTO> CreateUserFromRegistration(RequestCreateGymUserDTO createGymUserDTO);
        Task<string> GetCorrectFormatedEmail(string email);
        Task<GymUserDTO> GetUserByEmail(string email);
    }
}
