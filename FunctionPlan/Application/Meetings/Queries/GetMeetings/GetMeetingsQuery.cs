using Application.Abstractions.Messaging;

namespace Application.Meetings.Queries.GetMeetings
{
    public sealed record GetMeetingsQuery(
        string? SearchTerm,
        string? SortOrder
    ): ICommand<List<MeetingListDto>>;
}
