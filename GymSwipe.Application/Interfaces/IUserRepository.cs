using GymSwipe.ApplicationLayer.DTOs;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(GymUserDTO dto);
        Task<GymUserDTO> GetUserById(int id);
        Task DeleteUserById(int id);
    }
}
