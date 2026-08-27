using MediatR;

namespace Application.Media.Queries.GetMeetingMedia
{
    public sealed record GetMeetingMediaQuery
    (
        int UserId,
        int MeetingId
     ):IRequest<List<MeetingMediaDto>>;
}
