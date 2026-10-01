using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserService
    {
        Task<GymUserDTO?> TryAndCreateUserThroughRequestModelAsync(RequestCreateGymUserDTO request);
        Task<GymUserDTO> GetUserById(int id);
        Task<GymUserDTO> GetUserByEmail(string email);
        Task TryToDeleteUserById(int id);
        Task<string> GenerateFriendCode();
    }
}
