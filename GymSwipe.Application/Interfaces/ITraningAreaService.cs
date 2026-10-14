using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITraningAreaService
    {
        Task<List<TargetAreaDTO>> GetExerciseTargetsAsync();
        Task<List<TargetAreaDTO>> GetExerciseTargetAreasByIdAsync(int id);
        Task<List<TargetAreaDTO>> ConvertToDTOsFromDomainAsync(List<ExerciseTargetArea> areas);
        Task<TargetAreaDTO> GetTargetAreaByName(string name);
        Task<bool> AddNewArea(TargetAreaDTO targetAreaDTO);
        Task<string> GetTargetAreaImage(int id);

    }
}
