using GymSwipe.ApplicationLayer.DTOs.RequestDTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSwipe.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RequestCreateGymUserDTO request)
        {

            if (request == null)
            {
                return BadRequest(/*result*/);

            }
            await _userService.TryAndCreateUserThroughRequestModelAsync(request);

            return Ok();
        }
    }
}