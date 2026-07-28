using Application.Meetings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/meetings")]
    [ApiController]
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
    }
}
