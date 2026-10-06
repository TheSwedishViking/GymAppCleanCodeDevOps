using GymSwipe.ApplicationLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymSwipe.API.Controllers
{
    [ApiController]
    [Route("api/GymplayList")]
    public class GymplaylistController : ControllerBase
    {

        private readonly IGymPlaylistService _playlistService;

        public GymplaylistController(IGymPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylist(int id)
        {
            var playlist = await _playlistService.GetPlaylist(id);
            return Ok(playlist);
        }


    }
}




