using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Meetings.Queries.GetPastMeetings
{
    public sealed record GetPastMeetingsQuery(
        MeetingFiltersDto Filters
    ) : ICommand<List<MeetingListDto>>;
}
