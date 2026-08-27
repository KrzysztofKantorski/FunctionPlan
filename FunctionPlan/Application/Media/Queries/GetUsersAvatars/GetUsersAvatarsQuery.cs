using Application.Abstractions.Storage;
using MediatR;

namespace Application.Media.Queries.GetUsersAvatars
{
    public sealed record GetUsersAvatarsQuery(
        int MeetingId, 
        int UserId,
        string ImageId
    ):IRequest<FileResponse>;
}
