
using Application.Meetings.Queries.GetOrganizedMeetings;
using MediatR;

namespace Application.Meetings.Queries.GetAttendeedMeetings
{
    public sealed record GetOrganizedMeetingsQuery
    (
        string? SearchTerm,
        string? SortOrder,
        DateTime? StartDate,
        DateTime? EndDate,
        int userId
    ) :IRequest<List<MeetingListDto>>;
}
