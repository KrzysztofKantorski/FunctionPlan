using Application.Abstractions.Messaging;

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
