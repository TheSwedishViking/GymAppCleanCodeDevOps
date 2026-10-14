using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces.Services
{
    public interface ITraningAreaService
    {
        Task<List<TargetAreaDTO>> GetExerciseTargetsAsync();
        Task<TargetAreaDTO> GetExerciseTargetAreasByIdAsync(int id);
        Task<TargetAreaDTO> GetTargetAreaByName(string name);
        Task<bool> AddNewArea(TargetAreaDTO targetAreaDTO);
        Task<string> GetTargetAreaImage(int id);

    }
}
