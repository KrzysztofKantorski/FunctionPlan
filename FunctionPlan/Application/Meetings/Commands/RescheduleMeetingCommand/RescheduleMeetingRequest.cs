namespace Application.Meetings.Commands.RescheduleMeetingCommand
{
    public sealed record RescheduleMeetingRequest
    (
        int MeetingId,
        DateTime ScheduledFor
    );
}
