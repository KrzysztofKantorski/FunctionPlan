using Application.Common.Dto;
using MediatR;

namespace Application.Meetings.Queries.GetMyMeetings
{
    public sealed record GetMyMeetingsQuery(
    int userId
    ): IRequest<List<MeetingListDto>>;
}
