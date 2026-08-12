using Application.Abstractions.Messaging;

namespace Application.Meetings.Commands.ConfirmAttendenceCommand
{
    public sealed record ConfirmAttendenceCommand
    (
        int MeetingId,
        int UserId
    ): ICommand;
}
