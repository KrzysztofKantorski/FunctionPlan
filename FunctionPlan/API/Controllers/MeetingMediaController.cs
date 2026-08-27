using API.Extensions;
using Application.Common.Dto;
using Application.Media.Commands.AddMediaFile;
using Application.Media.Commands.ChangeImageDescription;
using Application.Media.Commands.RemoveMediaFile;
using Application.Media.Queries.GetMeetingMedia;
using Application.Media.Queries.GetMeetingMediaByImageId;
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
            var query = new GetMeetingMediaQuery
            (
                User.GetUserId(),
                MeetingId
            );

            var meetingMediaData = await _sender.Send(query, cancellationToken);

            return Ok(meetingMediaData);
        }


        //Get meeting media (specific image)
        [HttpGet("{MeetingId}/media/{ImageId}")]
        public async Task<IActionResult> GetMeetingImage(
            [FromRoute] int MeetingId,
            [FromRoute] string ImageId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetMeetingMediaByImageQuery(
                User.GetUserId(),
                MeetingId,
                ImageId
            );

            var image = await _sender.Send(query, cancellationToken);

            return File(image.Stream, image.ContentType);
        }


        //Remove media from meeting
        [HttpDelete("{MeetingId}/media/{ImageId}")]
        public async Task<IActionResult> RemoveMeetingFile(
            [FromRoute] int MeetingId,
            [FromRoute] string ImageId,
            CancellationToken cancellationToken
            )
        {
            var command = new RemoveMediaFileCommand
            (
                MeetingId,
                User.GetUserId(),
                ImageId
            );

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }


        //Change meeting image description
        [HttpPatch("{MeetingId}/media/{ImageId}")]
        public async Task<IActionResult> ChangeImageDescription(
            [FromRoute] int MeetingId,
            [FromRoute] string ImageId,
            [FromBody] string Description,
            CancellationToken cancellationToken
            )
        {
            var command = new ChangeMediaDescriptionCommand
            (
                MeetingId,
                User.GetUserId(),
                ImageId,
                Description
            );

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
