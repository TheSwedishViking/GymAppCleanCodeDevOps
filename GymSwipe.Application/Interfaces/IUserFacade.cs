using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface IUserFacade
    {
        Task<GymUserDTO> GetGymUserDTOAsync(int id);
        Task CreateGymUser(RequestCreateGymUserDTO request);
    }
}
