namespace Application.Meetings.Commands.CreateMeetingCommand
{
    public sealed record MeetingRequestDto
    (
        string Title,
        DateTime ScheduledFor,
        double Latitude,
        double Longitude
    );
}
