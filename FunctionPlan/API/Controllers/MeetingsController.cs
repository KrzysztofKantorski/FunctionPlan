using Application.Meetings.Commands;
using Application.Meetings.Queries.GetMeetingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers
{
    [ApiController]
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
            [FromBody] CreateMeetingCommand command,
            CancellationToken cancellationToken)
        {
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
