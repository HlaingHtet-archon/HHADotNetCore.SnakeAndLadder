using Microsoft.AspNetCore.Mvc;
using SnakeAndLadder.Domain.Models;

namespace SnakeAndLadder.RestApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.IsValidationError)
                return BadRequest(model);

            if (model.IsSystemError)
                return StatusCode(500, model);

            if (model.IsNotFound)
            {
                return NotFound(model);
            }

            return Ok(model);
        }
    }
}
