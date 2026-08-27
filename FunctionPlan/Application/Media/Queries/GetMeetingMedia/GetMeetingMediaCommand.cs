using MediatR;

namespace Application.Media.Queries.GetMeetingMedia
{
    public sealed record GetMeetingMediaCommand
    (
        int UserId,
        int MeetingId
     ):IRequest<List<MeetingMediaDto>>;
}
