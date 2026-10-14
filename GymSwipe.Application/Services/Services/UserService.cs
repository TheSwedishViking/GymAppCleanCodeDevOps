using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.Services;

namespace GymSwipe.ApplicationLayer.Services.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task<string> GenerateFriendCode()
        {
            string friendCode = "";
            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    friendCode += Random.Shared.Next(0, 9);
                }
                if (i != 3)
                {
                    friendCode += "-";
                }
            }
            return Task.FromResult(friendCode);
        }


        public async Task<GymUserDTO> GetUserById(int id)
        {
            return await _repo.GetUserById(id);
        }

        public async Task<GymUserDTO> GetCreatedUserByEmail(string email)
        {
            return await _repo.GetCreatedUserByEmail(email);
        }

        public async Task<bool> UserEmailIsTaken(string email)
        {
            return await _repo.UserEmailIsTaken(email);
        }


        public async Task TryToDeleteUserById(int id)
        {
            await _repo.DeleteUserById(id);

        }
        public async Task<GymUserDTO>? TryAndCreateUserThroughRequestModelAsync(RequestCreateGymUserDTO request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            GymUserDTO dto = new()
            {
                Firstname = request.Firstname,
                Surname = request.Surname,
                HeightCm = request.HeightCm,
                WeightKg = request.WeightKg,
                Email = request.Email,
                Gender = request.Gender
            };
            //User returns an id and friendcode, use on DTO
            var user = await _repo.AddUser(dto);

            dto.Id = user.Id;
            dto.FriendCode = user.FriendCode;
            return dto;
        }


    }
}