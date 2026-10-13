using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<GymUser> AddUser(GymUserDTO dto);
        Task<GymUserDTO> GetUserById(int id);
        Task<bool> UserEmailIsTaken(string email);
        Task DeleteUserById(int id);
        Task<GymUserDTO> GetCreatedUserByEmail(string email);
    }
}
