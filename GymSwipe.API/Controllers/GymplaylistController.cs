using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces.Services;
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


        [HttpGet("GetAllPlaylistsByUserId/{id}")]
        public async Task<ActionResult<List<GymPlaylistDTO>>> GetAllPlayListsByUserId(int id)
        {
            var playlist = await _playlistService.GetPlaylistDTOsByUserId(id);
            return Ok(playlist);
        }

        [HttpGet("GetPlaylistBy/{id}")]
        public async Task<ActionResult<List<GymPlaylistDTO>>> GetPlayList(int id)
        {
            var playlist = await _playlistService.GetPlaylist(id);
            return Ok(playlist);
        }


    }
}




