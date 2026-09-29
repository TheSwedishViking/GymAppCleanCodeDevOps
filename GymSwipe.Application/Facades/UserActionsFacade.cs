using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Interfaces;
using GymSwipe.Domain.Models;

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
        public async Task CreateGymUser(RequestCreateGymUserDTO request)
        {
            var existing = await _userService.GetUserByEmail(request.Email);
            //Return other error code if its over an API, or inform user of email already used

            //GymUser.CurrentUser = new 
            //{
            //    Id = Random.Shared.Next(1, 51231), //Replace later
            //    Firstname = request.Firstname,
            //    Surname = request.Surname,
            //    Email = request.Email,
            //    Gender = request.Gender,
            //    HeightCm = request.HeightCm,
            //    WeightKg = request.WeightKg,
            //    //FriendCode = await _userService.GenerateFriendCode()
            //};

            await _userRepository.AddUser(GymUser.CurrentUser);
            //return dto;
        }

        public Task<GymUserDTO> GetGymUserDTOAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
