using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.ApplicationLayer.Services
{
    public class TrainingAreaService : ITraningAreaService
    {
        private readonly ITraningAreaRepo _repo;
        public TrainingAreaService(ITraningAreaRepo traningAreaRepo)
        {
            _repo = traningAreaRepo;
        }

        public async Task<List<TargetAreaDTO>> ConvertToDTOsFromDomainAsync(List<ExerciseTargetArea> areas)
        {
            List<TargetAreaDTO> dtos = areas.Select(e =>
            new TargetAreaDTO
            {
                Id = e.Id,
                Name = e.ExerciseCategoryName,
            }).ToList();

            return dtos;
        }

        public async Task<List<TargetAreaDTO>> GetExerciseTargetsAsync()
        {
            var areas = await _repo.GetExerciseTargetAreasAsync();

            var dtos = await ConvertToDTOsFromDomainAsync(areas);

            return dtos;

        }
    }
}
