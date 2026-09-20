using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Meetings.Queries.GetMeetings
{
    public sealed record GetMeetingsQuery(
        int UserId,
        string? SearchTerm,
        DateTime? StartDate,
        DateTime? EndDate,
        string? SortOrder
    ): ICommand<List<MeetingListDto>>;
}
