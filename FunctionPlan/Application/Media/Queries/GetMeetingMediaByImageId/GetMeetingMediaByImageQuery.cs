using Application.Abstractions.Storage;
using MediatR;

namespace Application.Media.Queries.GetMeetingMediaByImageId
{
    public sealed record GetMeetingMediaByImageQuery(
        int UserId,
        int MeetingId,
        string ImageId
    ): IRequest<FileResponse>;
}
