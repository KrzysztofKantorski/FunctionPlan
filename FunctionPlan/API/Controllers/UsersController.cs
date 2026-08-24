using API.Extensions;
using Application.Users.Commands.UploadUserImage;
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



        //Get user by id
        [HttpGet("me")]
        public async Task<IActionResult> GetMeetingById(
            CancellationToken cancellationToken
            )
        {
            var command = new GetUserDetailsQuery(User.GetUserId());
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }


        //Upload user avatar
        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadUserImage(
            IFormFile file,
            CancellationToken cancelToken
            )
        {

            if (file is null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }

            using var stream = file.OpenReadStream();

            var fileDto = new FileDto(
                stream,
                file.FileName,
                file.ContentType
            );

            var command = new UploadUserImageCommand
            (
                fileDto,
                User.GetUserId()
            );

            await _sender.Send(command, cancelToken);
            return NoContent();
        }
    }
}
