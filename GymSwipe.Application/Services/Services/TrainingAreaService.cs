using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Mappers;
using GymSwipe.ApplicationLayer.Interfaces.Repositories;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services.Services
{
    public class TrainingAreaService : ITraningAreaService
    {
        private readonly ITraningAreaRepo _repo;
        private readonly IMapDTOService _mapDTOService;
        public TrainingAreaService(
            ITraningAreaRepo traningAreaRepo,
            IMapDTOService mapDTOService
            )
        {
            _repo = traningAreaRepo;
            _mapDTOService = mapDTOService;
        }

        public async Task<bool> AddNewArea(TargetAreaDTO targetAreaDTO)
        {
            var domain = new ExerciseTargetArea
            {
                ExerciseCategoryName = targetAreaDTO.Name
            };
            var result = await _repo.AddTargetArea(domain);
            return result;
        }

        public async Task<TargetAreaDTO> GetExerciseTargetAreasByIdAsync(int id)
        {
            var area = await _repo.GetAreaById(id);
            if(area == null)
            {
                return null;
            }
            var dto = await _mapDTOService.MapTargetAreaDTO(area);
            return dto;
        }

        public async Task<List<TargetAreaDTO>> GetExerciseTargetsAsync()
        {
            var areas = await _repo.GetExerciseTargetAreasAsync();
            if(areas == null || areas.Count == 0)
            {
                return null;
            }
            var dtos = (await Task.WhenAll(areas.Select(_mapDTOService.MapTargetAreaDTO))).ToList();

            return dtos;

        }

        public async Task<TargetAreaDTO> GetTargetAreaByName(string name)
        {
            var ex = await _repo.GetExerciseTargetAreaByName(name);
            if (ex == null)
            {
                return null;
            }
            var dto = await _mapDTOService.MapTargetAreaDTO(ex);
            return dto;
        }

        public async Task<string> GetTargetAreaImage(int id)
        {
            return await _repo.GetTargetAreaImage(id);
        }

       
    }
}
