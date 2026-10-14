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
    }
}
