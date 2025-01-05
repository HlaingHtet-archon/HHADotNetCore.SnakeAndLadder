using Microsoft.AspNetCore.Mvc;
using SnakeAndLadder.Domain.features;
using SnakeAndLadder.Domain.Models;

namespace SnakeAndLadder.RestApi.Controllers
{
    public class BoardController : BaseController
    {
        private readonly IBoardService _service;

        public BoardController(IBoardService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardRequestModel newBoard)
        {
            try
            {
                var result = await _service.CreateBoard(newBoard);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoard(int id)
        {
            try
            {
                var result = await _service.GetBoardById(id);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            try
            {
                var result = await _service.GetBoards();
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(int id, BoardRequestModel board)
        {
            try
            {
                var result = await _service.UpdateBoard(id, board);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(int id)
        {
            try
            {
                var result = await _service.DeleteBoard(id);
                return Execute(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
