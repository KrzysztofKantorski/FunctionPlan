using Application.Abstractions.Messaging;

namespace Application.Meetings.Queries.GetPastMeetings
{
    public sealed record GetPastMeetingsQuery(
        string? SearchTerm,
        string? SortOrder
    ) : ICommand<List<MeetingListDto>>;
}
