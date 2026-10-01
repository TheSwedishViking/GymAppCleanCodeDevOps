using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> PostNewExercise([FromBody] ExerciseDTO exercise)
        {
            if(exercise == null)
            {
                return BadRequest();
            }

            //Add to service later

            return Ok();
        }

        [HttpGet("get-all-exercises", Name = "GetAllExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetExerciseDTOsAsync()
        {
            var exs = await _exerciseService.GetAllExercisesAsync();
            if(exs == null)
            {
                return NotFound("No exercieses found; Db might be empty");
            }
            return Ok(exs);
        }

        [HttpGet("get-chest-exercises", Name ="GetOnlyChestExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetChestExerciesDTOsAsync()
        {
            var chest = await _exerciseService.GetExerciseByIdAsync(1);
            if(chest == null)
            {
                return NotFound("No chest exercies found; check db or connection string");
            }
            return Ok(chest);
        }

        [HttpGet("get-random-exercises", Name ="GetRandomExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetRandomExercisesAsync()
        {
            var random = await _exerciseService.GetRandomExercisesAsync();
            if(random == null)
            {
                return NotFound("No random was found; Db might be empty");
            }
            return Ok(random);
        }
        [HttpGet("get-exercises/{id}", Name = "GetSpecificExercises")]
        public async Task<ActionResult<List<ExerciseDTO>>> GetRandomExercisesAsync( int id)
        {
            var random = await _exerciseService.GetExerciseByIdAsync(id);
            if (random == null)
            {
                return NotFound("No random was found; Db might be empty");
            }
            return Ok(random);
        }

    }
}
