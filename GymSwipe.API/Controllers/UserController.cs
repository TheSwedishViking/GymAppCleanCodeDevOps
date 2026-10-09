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
            var user = await _userService.TryAndCreateUserThroughRequestModelAsync(request);
            return Created("CreatedUser", user);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            await _userService.TryToDeleteUserById(id);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user); //return the user objekt to api 
        }

        [HttpGet("Get-UserDto-By-Email/{email}")]
        public async Task<IActionResult> GetCreatedUserByEmail(string email)
        {
            var user = await _userService.GetCreatedUserByEmail(email);
            return Ok(user);
        }

        [HttpGet("Unique-Email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            bool avaliableEmail = await _userService.GetUserByEmail(email);
            return Ok(avaliableEmail);
        }
    }
}
