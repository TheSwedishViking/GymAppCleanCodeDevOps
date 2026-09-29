using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(GymUser dto);
        Task<GymUser> GetUserById(int id);

    }
}
