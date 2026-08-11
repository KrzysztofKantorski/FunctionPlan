using Application.Abstractions.Messaging;

namespace Application.Meetings.Commands.CancellMeetingCommand
{
    public sealed record CancelMeetingCommand
    (
        int MeetingId,
        int OrganizerId
    ): ICommand;
}
