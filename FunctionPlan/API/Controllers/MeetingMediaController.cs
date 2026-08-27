using API.Extensions;
using Application.Common.Dto;
using Application.Media.Commands.AddMediaFile;
using Application.Media.Queries.GetMeetingMedia;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [EnableRateLimiting("GlobalLimit")]
    [Route("api/meetings")]
    public class MeetingMediaController : ControllerBase
    {
        private ISender _sender;
        public MeetingMediaController(ISender sender)
        {
            _sender = sender;
        }


        //Add media to meeting
        [HttpPost("{MeetingId}/media")]
        public async Task<IActionResult> AddMedia(
            [FromRoute] int MeetingId,
            [FromForm] string? Description,
            IFormFile file,
            CancellationToken cancellationToken
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

            var command = new AddMediaFileCommand
            (
                User.GetUserId(),
                MeetingId,
                Description,
                fileDto
            );

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }


        //Get meeting media (only image url's)
        [HttpGet("{MeetingId}/media")]
        public async Task<IActionResult> GetMeetingMedia(
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            )
        {
            var command = new GetMeetingMediaQuery
            (
                User.GetUserId(),
                MeetingId
            );

            var meetingMediaData = await _sender.Send(command, cancellationToken);

            return Ok(meetingMediaData);
        }
    }
}
