using Application.Abstractions.Messaging;

namespace Application.Meetings.Commands.RescheduleMeetingCommand
{
    public sealed record RescheduleMeetingCommand(
        int MeetingId,
        int OrganizerId,
        DateTime ScheduledFor
    ) : ICommand;
}
