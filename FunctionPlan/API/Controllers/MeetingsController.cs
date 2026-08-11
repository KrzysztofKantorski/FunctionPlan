using Application.Meetings.Commands.CancellMeetingCommand;
using Application.Meetings.Commands.ChangeCoordinates;
using Application.Meetings.Commands.CreateMeetingCommand;
using Application.Meetings.Commands.RescheduleMeetingCommand;
using Application.Meetings.Queries.GetMeetingById;
using Domain.Meetings;
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


        //Create meeting

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


        //Get meeting by id

        [HttpGet("{MeetingID}")]
        public async Task<IActionResult> GetMeetingById(
            [FromRoute] int MeetingId

            )
        {
            var result = await _sender.Send(new GetMeetingByIdQuery(MeetingId));

            return Ok(new { id = result });
        }


        //Reschedule

        [HttpPatch("{MeetingId}/reschedule")]
        public async Task<IActionResult> ReacheduleMeeting(
            [FromRoute] int MeetingId,
            [FromBody] RescheduleMeetingRequest request,
            CancellationToken cancellationToken
            )
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int organizerId))
            {
                return Unauthorized("Incorrect token");
            }

            var command = new RescheduleMeetingCommand(
                MeetingId,
                organizerId,
                request.ScheduledFor
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


        //Cancel
        [HttpPatch("{MeetingId}/cancel")]
        public async Task<IActionResult> CancelMeeting(
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            ) 
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int organizerId))
            {
                return Unauthorized("Incorrect token");
            }

            var command = new CancelMeetingCommand(
                MeetingId,
                organizerId
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


        //Change coordinates
        [HttpPatch("{MeetingId}/coordinates")]
        public async Task<IActionResult> ChangeMeetingCoordinates(
            [FromBody] ChangeCoordinatesRequestDto request,
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            )
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int organizerId))
            {
                return Unauthorized("Incorrect token");
            }

            var command = new ChangeCoordinatesCommand(
                MeetingId,
                organizerId,
                request.Longitude,
                request.Latitude
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
