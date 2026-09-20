using Application.Common.Dto;
using MediatR;

namespace Application.Meetings.Queries.GetMyMeetings
{
    public sealed record GetMyMeetingsQuery(
        MeetingFiltersDto Filters,
        int userId
    ): IRequest<List<MeetingListDto>>;
}
