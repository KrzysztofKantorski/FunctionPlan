namespace Application.Meetings.Commands.CreateMeetingCommand
{
    public sealed record MeetingRequestDto
    (
        string Title,
        string Description,
        DateTime ScheduledFor,
        double Latitude,
        double Longitude
    );
}
