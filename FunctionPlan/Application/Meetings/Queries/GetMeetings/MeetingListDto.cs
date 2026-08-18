namespace Application.Meetings.Queries.GetMeetings
{
    public sealed record MeetingListDto(
        int Id,
        string Title,
        DateTime ScheduledFor,
        int OrganizerId,
        string OrganizerName
    );
}
