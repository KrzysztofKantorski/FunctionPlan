namespace Application.Meetings.Queries.GetMeetingById
{
    public sealed record MeetingDto
    (
        int Id,
        string Title,
        DateTime ScheduledFor,
        double Latitude,
        double Longitude
    );
}
