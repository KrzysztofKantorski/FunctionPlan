using Application.Abstractions.Messaging;

namespace Application.Meetings.Commands.CancelMeetingAttendance.cs
{
    public sealed record CancelMeetingAttendanceCommand(
        int MeetingId,
        int UserId
    ):ICommand;
}
