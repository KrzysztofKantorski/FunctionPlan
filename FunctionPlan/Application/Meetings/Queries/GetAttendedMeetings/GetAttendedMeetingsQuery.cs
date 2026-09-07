using Application.Common.Dto;
using MediatR;

namespace Application.Meetings.Queries.GetAttendeedMeetings
{
    public sealed record GetAttendedMeetingsQuery
    (
        int userId
    ):IRequest<List<MeetingListDto>>;
}
