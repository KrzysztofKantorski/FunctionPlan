namespace Application.Meetings.Commands
{
    public sealed record MeetingRequestDto
    (
        string Title,
        DateTime ScheduledFor,
        double Latitude,
        double Longitude
    );
}
