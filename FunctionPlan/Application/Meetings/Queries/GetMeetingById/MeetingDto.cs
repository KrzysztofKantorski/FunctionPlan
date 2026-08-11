namespace Application.Meetings.Queries.GetMeetingById
{
    public sealed class MeetingDto
    {
        public int Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public DateTime ScheduledFor { get; init; }
        public double Latitude { get; init; }
        public double Longitude { get; init; }
        public OrganizerDto Organizer { get; set; } = null!;
    }
}
