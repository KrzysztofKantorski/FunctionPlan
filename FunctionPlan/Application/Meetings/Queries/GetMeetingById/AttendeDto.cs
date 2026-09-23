namespace Application.Meetings.Queries.GetMeetingById
{
    public sealed class AttendeeDto
    {
        public int Id { get; init; }
        public string Username { get; init; } = string.Empty;
        public string? ProfilePictureUrl { get; init; }
    }
}
