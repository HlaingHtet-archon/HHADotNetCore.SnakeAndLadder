using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnakeAndLadder.Domain.features;
using SnakeAndLadder.Domain.Models;

namespace SnakeAndLadder.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _service;

        public PlayerController(IPlayerService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> CreatePlayer(PlayerRequestModel newPlayer)
        {
            try
            {
                var model = await _service.CreatePlayer(newPlayer);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            try
            {
                var model = await _service.GetPlayer(playerId);
                return Execute(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
