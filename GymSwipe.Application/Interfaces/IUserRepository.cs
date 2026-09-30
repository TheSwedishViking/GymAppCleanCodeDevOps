using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(GymUserDTO dto);
        Task<GymUser> GetUserById(int id);
        Task DeleteUserById(int id);
    }
}
