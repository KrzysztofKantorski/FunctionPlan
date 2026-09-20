using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Meetings.Queries.GetMeetings
{
    public sealed record GetMeetingsQuery(
        MeetingFiltersDto Filters,
        int UserId
    ): ICommand<List<MeetingListDto>>;
}
