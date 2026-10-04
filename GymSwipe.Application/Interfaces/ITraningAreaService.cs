using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Interfaces
{
    public interface ITraningAreaService
    {
        Task<List<TargetAreaDTO>> GetExerciseTargetsAsync();
        Task<List<TargetAreaDTO>> ConvertToDTOsFromDomainAsync(List<ExerciseTargetArea> areas);
        Task<TargetAreaDTO> GetTargetAreaByName(string name);
        Task AddNewArea(TargetAreaDTO targetAreaDTO);
    }
}
