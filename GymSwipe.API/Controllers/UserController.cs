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
            //var result = await _userService.CreateGymUserAsync(request);
            //if (result.IsSuccess)
            //{
            //    return Ok(result);
            //}
            return BadRequest(/*result*/);

        }
    }
}