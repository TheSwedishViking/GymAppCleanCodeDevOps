using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<GymUser> AddUser(GymUserDTO dto);
        Task<GymUserDTO> GetUserById(int id);
        Task<bool> GetUserByEmail(string Email);
        Task DeleteUserById(int id);
    }
}
