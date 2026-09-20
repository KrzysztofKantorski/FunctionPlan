using Application.Common.Dto;
using MediatR;

namespace Application.Meetings.Queries.GetMyMeetings
{
    public sealed record GetMyMeetingsQuery(
    int userId,
    string? SearchTerm,
    string? SortOrder,
    DateTime? StartDate,
    DateTime? EndDate
    ): IRequest<List<MeetingListDto>>;
}
