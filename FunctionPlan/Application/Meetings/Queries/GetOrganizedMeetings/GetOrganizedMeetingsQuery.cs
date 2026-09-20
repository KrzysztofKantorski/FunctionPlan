using Application.Common.Dto;
using Application.Meetings.Queries.GetOrganizedMeetings;
using MediatR;

namespace Application.Meetings.Queries.GetAttendeedMeetings
{
    public sealed record GetOrganizedMeetingsQuery
    (
        MeetingFiltersDto Filters,
        int userId
    ) :IRequest<List<OrganizedMeetingListDto>>;
}
