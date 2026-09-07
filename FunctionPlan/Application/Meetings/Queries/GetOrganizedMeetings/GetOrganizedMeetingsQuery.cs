using Application.Common.Dto;
using MediatR;

namespace Application.Meetings.Queries.GetAttendeedMeetings
{
    public sealed record GetOrganizedMeetingsQuery
    (
        int userId
    ):IRequest<List<MeetingListDto>>;
}
