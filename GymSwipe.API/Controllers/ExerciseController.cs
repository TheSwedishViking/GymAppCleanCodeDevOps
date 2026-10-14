using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSwipe.API.Controllers
{
    [ApiController]
    [Route("api/Exercise")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet("get-all-exercises", Name = "GetAllExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetExerciseDTOsAsync()
        {
            var exs = await _exerciseService.GetAllExercisesAsync();
            if (exs == null)
            {
                return NotFound("No exercieses found; Db might be empty");
            }
            return Ok(exs);
        }

        [HttpGet("get-chest-exercises", Name = "GetOnlyChestExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetChestExerciesDTOsAsync()
        {
            var chest = await _exerciseService.GetExercisesByCategoryIdAsync(1);
            if (chest == null)
            {
                return NotFound("No chest exercies found; check db or connection string");
            }
            return Ok(chest);
        }

        [HttpGet("get-category-exercises/{id}", Name = "GetTargetAreaExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetTargetAreaExerciesDTOsAsync(int id)
        {
            var areaExercises = await _exerciseService.GetExercisesByCategoryIdAsync(id);
            if (areaExercises == null)
            {
                return NotFound("No exercises found for the specified category");
            }
            return Ok(areaExercises);
        }

        [HttpGet("get-random-exercises", Name = "GetRandomExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetRandomExercisesAsync()
        {
            var random = await _exerciseService.GetRandomExercisesAsync();
            if (random == null)
            {
                return NotFound("No random was found; Db might be empty");
            }
            return Ok(random);
        }
        [HttpGet("get-exercises-by-category/{id}", Name = "GetSpecificExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetRandomExercisesAsync(int id)
        {
            var random = await _exerciseService.GetExercisesByCategoryIdAsync(id);
            if (random == null)
            {
                return NotFound("No random was found; Db might be empty");
            }
            return Ok(random);
        }

    }
}
