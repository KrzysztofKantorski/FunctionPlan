using Application.Abstractions.Messaging;

namespace Application.Meetings.Queries.GetMeetingAttendeesQuery
{
    public sealed record GetMeetingAttendeesQuery
    (
        int UserId,
        int MeetingId
        ):ICommand<List<AttendeeDto>>;
}
