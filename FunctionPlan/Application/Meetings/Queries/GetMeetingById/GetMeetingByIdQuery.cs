using MediatR;

namespace Application.Meetings.Queries.GetMeetingById
{
    public sealed record GetMeetingByIdQuery
    (
        int MeetingId
    ): IRequest<MeetingDto>;
}
