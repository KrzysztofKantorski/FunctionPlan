namespace Application.Meetings.Queries.GetMeetings
{
    public sealed class MeetingListDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public DateTime ScheduledFor { get; init; }
        public int OrganizerId { get; init; }
        public string OrganizerName { get; init; }
    };
}
