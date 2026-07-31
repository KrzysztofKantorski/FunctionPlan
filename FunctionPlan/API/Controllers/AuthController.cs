using Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [Route("api/auth")]
    [EnableRateLimiting("GlobalLimit")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ISender _sender;
        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
           [FromBody] RegisterUserCommand command,
           CancellationToken cancellationToken)
        {
            int userId = await _sender.Send(command, cancellationToken);
            return Created("", new { Id = userId });
        }
    }
}
