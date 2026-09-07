using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Meetings.Queries.GetMeetings
{
    public sealed record GetMeetingsQuery(
        string? SearchTerm,
        DateTime? StartDate,
        DateTime? EndDate,
        string? SortOrder,
        int? Status
    ): ICommand<List<MeetingListDto>>;
}
