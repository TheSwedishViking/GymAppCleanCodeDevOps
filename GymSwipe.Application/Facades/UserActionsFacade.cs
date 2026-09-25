using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Facades
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
            if (existing is not null) return null; //Return other error code if its over an API, or inform user of email already used

            var dto = new GymUserDTO
            {
                Id = Random.Shared.Next(1, 51231), //Replace later
                Firstname = request.Firstname,
                Surname = request.Surname,
                Email = request.Email,
                Gender = request.Gender,
                HeightCm = request.HeightCm,
                WeightKg = request.WeightKg,
                FriendCode = await _userService.GenerateFriendCode()
            };

            await _userRepository.AddUser(dto);
            return dto;
        }

        public Task<GymUserDTO> GetGymUserDTOAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
