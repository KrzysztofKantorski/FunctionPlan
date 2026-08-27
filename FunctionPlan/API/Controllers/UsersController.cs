using API.Extensions;
using Application.Common.Dto;
using Application.Media.Queries.GetUsersAvatars;
using Application.Users.Commands.UploadUserImage;
using Application.Users.Queries.GetUserDetailsQuery;
using Application.Users.Queries.GetUserImageQuery;
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


        //Get user avatar
        [HttpGet("avatar")]
        public async Task<IActionResult> GetUserImage(
            CancellationToken cancellationToken
            )
        {
            int userId = User.GetUserId();

            var query = new GetUserImageQuery(userId);

            var fileResponse = await _sender.Send(query, cancellationToken);

            return File(fileResponse.Stream, fileResponse.ContentType);
        }

        //Upload user avatar
        [EnableRateLimiting("AvatarUploader")]
        [HttpPost("avatar")]
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


        //Get meeting attendees info (username, avatar)
        [HttpGet("avatar/{AvatarId}")]
        public async Task<IActionResult> GetMeetingMediaAvatars
            (
            [FromRoute] string AvatarId,
            CancellationToken cancellationToken
            )
        {
            var query = new GetUsersAvatarsQuery(
                User.GetUserId(),
                AvatarId
            );

            var image = await _sender.Send(query, cancellationToken);

            return File(image.Stream, image.ContentType);
        }
    }
}
