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

        public async Task AddNewArea(TargetAreaDTO targetAreaDTO)
        {
            var domain = new ExerciseTargetArea
            {
                ExerciseCategoryName = targetAreaDTO.Name
            };
            await _repo.AddTargetArea(domain);
        }

        public async Task<List<TargetAreaDTO>> ConvertToDTOsFromDomainAsync(List<ExerciseTargetArea> areas)
        {

            var dtos = (await Task.WhenAll(areas.Select(e=>ConvertToDTO(e)))).ToList();

            return dtos;
        }

        public async Task<List<TargetAreaDTO>> GetExerciseTargetsAsync()
        {
            var areas = await _repo.GetExerciseTargetAreasAsync();

            var dtos = await ConvertToDTOsFromDomainAsync(areas);

            return dtos;

        }

        public async Task<TargetAreaDTO> GetTargetAreaByName(string name)
        {
            var ex = await _repo.GetExerciseTargetAreaByName(name);
            if (ex == null)
            {
                return null;
            }
            return await ConvertToDTO(ex);
        }

        private async Task<TargetAreaDTO> ConvertToDTO(ExerciseTargetArea ex)
        {
            return new TargetAreaDTO
            {
                Id = ex.Id,
                Name = ex.ExerciseCategoryName,
            };
        }
    }
}
