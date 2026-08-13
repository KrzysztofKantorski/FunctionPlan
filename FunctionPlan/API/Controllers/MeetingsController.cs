using API.Extensions;
using Application.Meetings.Commands.CancellMeetingCommand;
using Application.Meetings.Commands.CancelMeetingAttendance.cs;
using Application.Meetings.Commands.ChangeCoordinates;
using Application.Meetings.Commands.ConfirmAttendenceCommand;
using Application.Meetings.Commands.CreateMeetingCommand;
using Application.Meetings.Commands.RescheduleMeetingCommand;
using Application.Meetings.Queries.GetMeetingAttendeesQuery;
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

            var command = new CreateMeetingCommand(
                request.Title,
                request.ScheduledFor,
                User.GetUserId(),
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


        //Get meeting attendees
        [HttpGet("{MeetingID}/attendees")]
        public async Task<IActionResult> MeetingAttendees(
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            )
        {
            var command = new GetMeetingAttendeesQuery(MeetingId);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }


        //Reschedule

        [HttpPatch("{MeetingId}/reschedule")]
        public async Task<IActionResult> ReacheduleMeeting(
            [FromRoute] int MeetingId,
            [FromBody] RescheduleMeetingRequest request,
            CancellationToken cancellationToken
            )
        {
           
            var command = new RescheduleMeetingCommand(
                MeetingId,
                User.GetUserId(),
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

            var command = new CancelMeetingCommand(
                MeetingId,
                User.GetUserId()
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

            var command = new ChangeCoordinatesCommand(
                MeetingId,
                User.GetUserId(),
                request.Longitude,
                request.Latitude
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


        //Confirm meeting attendance
        [HttpPost("{MeetingId}/attendees")]
        public async Task<IActionResult> ConfirmAttendence(
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            )
        {

            var command = new ConfirmAttendenceCommand(
                MeetingId,
                User.GetUserId()
            );

            await _sender.Send(command, cancellationToken);

            return NoContent();
        }


        //Cancel meeting attendance
        [HttpDelete("{MeetingId}/attendees")]
        public async Task<IActionResult> CancelAttendance(
            [FromRoute] int MeetingId,
            CancellationToken cancellationToken
            ) 
        {

            var command = new CancelMeetingAttendanceCommand(
                MeetingId,
                User.GetUserId()
            );

            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
    }
}
