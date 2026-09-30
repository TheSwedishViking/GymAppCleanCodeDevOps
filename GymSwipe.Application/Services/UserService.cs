using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;

namespace GymSwipe.ApplicationLayer.Services
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

        public Task<GymUserDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<GymUserDTO> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<GymUserDTO>? TryAndCreateUserThroughRequestModelAsync(RequestCreateGymUserDTO request)
        {
            GymUserDTO dto = new()
            {
                Firstname = request.Firstname,
                Surname = request.Surname,
                HeightCm = request.HeightCm,
                WeightKg = request.WeightKg,
                Email = request.Email,
                Gender = request.Gender
            };
            await _repo.AddUser(dto);

            //ytterligare logik kan ske här, dto är inte garanterad att sparas i databasen
            return dto;
        }
    }
}
