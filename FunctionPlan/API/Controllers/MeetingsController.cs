using Application.Meetings.Commands.CreateMeetingCommand;
using Application.Meetings.Queries.GetMeetingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/meetings")]
    [EnableRateLimiting("GlobalLimit")] 
    
    public sealed class MeetingsController : ControllerBase
    {
        private ISender _sender;
        public MeetingsController(ISender sender) 
        {
            _sender = sender;
        }


        [HttpPost]
        public async Task<IActionResult> CreateMeeting(
            [FromBody] MeetingRequestDto request,
            CancellationToken cancellationToken)
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int organizerId))
            {
                return Unauthorized("Incorrect token");
            }

            var command = new CreateMeetingCommand(
                request.Title,
                request.ScheduledFor,
                organizerId,
                request.Latitude,
                request.Longitude
            );

            int meetingId = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(CreateMeeting), new { id = meetingId }, meetingId);
        }

        [Authorize]
        [HttpGet("{MeetingID}")]
        public async Task<IActionResult> GetMeetingById(
            [FromRoute] int MeetingId

            )
        {
            var result = await _sender.Send(new GetMeetingByIdQuery(MeetingId));

            return Ok(result);
        }
    }
}
