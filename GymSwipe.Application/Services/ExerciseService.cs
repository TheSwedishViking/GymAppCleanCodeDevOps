using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Enums;
using GymSwipe.Domain.Models;

namespace GymSwipe.ApplicationLayer.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _repo;
        private readonly IMapDTOService _mapDTOService;
        private readonly IMapDomainService _mapDomainService;
        public ExerciseService(IExerciseRepository exerciseRepository, IMapDTOService mapDTo, IMapDomainService mapDomain)
        {
            _repo = exerciseRepository;
            _mapDomainService = mapDomain;
            _mapDTOService=mapDTo;
        }
        public async Task<List<ExerciseDTO>> GetAllExercisesAsync()
        {
            var exercises = await _repo.GetAllExercisesAsync();
            var dtos = (await Task.WhenAll(exercises.Select(_mapDTOService.MapExerciseDTO))).ToList();
            return dtos;
        }

        public async Task<List<ExerciseDTO>?> GetExerciseByEnum(AreaEnum area)
        {
            var exas = await GetExerciseByIdAsync((int)(area));
            return exas;
        }

        public async Task<List<ExerciseDTO>> GetExerciseByIdAsync(int id)
        {
            var categoryExercises = await _repo.GetExercisesByCategoryAsync(id);
            var dtos = (await Task.WhenAll(categoryExercises.Select(_mapDTOService.MapExerciseDTO))).ToList();
        
            return dtos;
        }

        public async Task<ExerciseDTO> GetExerciseByName(string name)
        {
            var ex = await _repo.GetExerciseByName(name);
            if (ex == null)
            {
                throw new Exception("No exercise found by that name");
            }
            var dto = await _mapDTOService.MapExerciseDTO(ex);
            return dto;
        }

        public async Task<List<ExerciseDTO>?> GetRandomExercisesAsync()
        {
            var dtos = await GetAllExercisesAsync();
            var arr = dtos.ToArray();
            var randoms = arr.Shuffle().Take(3).ToList();
            return randoms;
        }
      

        public async Task<bool> SaveExercise(ExerciseDTO newExercise)
        {
            var checkExisting = await GetExerciseByName(newExercise.Name);

            if (checkExisting != null)
            {
                return false;
            }
            var domain = await _mapDomainService.MapExerciseDomain(newExercise);
            return await _repo.AddExercise(domain);
        }
    }
}
