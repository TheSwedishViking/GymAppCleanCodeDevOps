using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserService
    {
        Task<GymUserDTO?> TryAndCreateUserThroughRequestModelAsync(RequestCreateGymUserDTO request);
        Task<GymUserDTO> GetUserById(int id);
        Task<bool> GetUserByEmail(string email);
        Task<GymUserDTO> GetCreatedUserByEmail(string email);
        Task TryToDeleteUserById(int id);
        Task<string> GenerateFriendCode();
    }
}
