using API.Extensions;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/users")]
    [EnableRateLimiting("GlobalLimit")]

    public sealed class UsersController : ControllerBase
    {
        private ISender _sender;
        public UsersController(ISender sender)
        {
            _sender = sender;
        }



        //Get meeting by id
        [HttpGet("me")]
        public async Task<IActionResult> GetMeetingById(
            CancellationToken cancellationToken
            )
        {
            var command = new GetUserDetailsQuery(User.GetUserId());
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

    }
}
