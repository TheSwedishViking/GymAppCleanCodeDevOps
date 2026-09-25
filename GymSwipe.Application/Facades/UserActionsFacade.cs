using GymSwipe.Application.DTOs;
using GymSwipe.Application.DTOs.RequestDTOs;
using GymSwipe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Facades
{
    public class UserActionsFacade : IUserFacade
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        public UserActionsFacade(IUserRepository userRepository, IUserService userService)
        {
            _userService = userService;
            _userRepository = userRepository;
        }
        public async Task<GymUserDTO?> CreateGymUser(RequestCreateGymUserDTO request)
        {
            var existing = await _userService.GetUserByEmail(request.Email);
            if (existing is not null) return null;
            throw new NotImplementedException();
        }

        public Task<GymUserDTO> GetGymUserDTOAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
