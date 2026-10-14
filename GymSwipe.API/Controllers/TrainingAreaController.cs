using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSwipe.API.Controllers
{
    [ApiController]
    [Route("api/TrainingArea")]
    public class TrainingAreaController:ControllerBase
    {
        private readonly ITraningAreaService _trainingAreaService;
        public TrainingAreaController(ITraningAreaService traningAreaService)
        {
            _trainingAreaService = traningAreaService;
        }

        [HttpGet("get-all-areas")]
        public async Task<ActionResult<List<TargetAreaDTO>>> GetTrainingAreaDTOsAsync()
        {
            var tras = await _trainingAreaService.GetExerciseTargetsAsync();
            if(tras == null)
            {
                return NotFound("No areas found; Possible null or db empty");
            }
            return Ok(tras);
        }
        [HttpGet("get-area-by-id/{id}")]
        public async Task<ActionResult<List<TargetAreaDTO>>> GetTrainingAreaDTOsByIdAsync(int id)
        {
            if(id == 0 || id < 0)
            {
                return BadRequest();
            }
            var tras = await _trainingAreaService.GetExerciseTargetAreasByIdAsync(id);
            if (tras == null)
            {
                return NotFound("No areas found; Possible null or db empty");
            }
            return Ok(tras);
        }
        [HttpGet("get-image/{id}")]
        public async Task<ActionResult<string>> GetAreaImage(int id)
        {
            if (id == 0 || id < 0)
            {
                return BadRequest();
            }
            var image = await _trainingAreaService.GetTargetAreaImage(id);
            if(image == null)
            {
                return NotFound();
            }
            //Returns a standard "Unknown" image when there's no predefined image
            return Ok(image);
        }
    }
}
