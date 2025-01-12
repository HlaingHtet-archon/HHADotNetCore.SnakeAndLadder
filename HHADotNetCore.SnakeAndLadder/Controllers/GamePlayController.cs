using Microsoft.AspNetCore.Mvc;
using SnakeAndLadder.Domain.features;
using SnakeAndLadder.Domain.Models;
using SnakeAndLadder.RestApi;
using System;
using System.Threading.Tasks;

namespace SnakeLadderAPI.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameplayService _service;

        public GameController(IGameplayService service)
        {
            _service = service;
        }

        [HttpPost("{gameCode}")]
        public async Task<IActionResult> AddPlayer(string gameCode, GameplayRequestModel request)
        {
            try
            {
                request.GameCode = gameCode;
                var result = await _service.AddPlayer(request);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{gameCode}/{playerCode}")]
        public async Task<IActionResult> RollDice(string gameCode, string playerCode, GameplayRequestModel request)
        {
            try
            {
                request.GameCode = gameCode;
                request.PlayerCode = playerCode;
                var result = await _service.RollDice(request);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{gameCode}")]
        public async Task<IActionResult> UpdateWinner(string gameCode, GameplayRequestModel request)
        {
            try
            {
                request.GameCode = gameCode;
                var result = await _service.UpdateWinner(request);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
