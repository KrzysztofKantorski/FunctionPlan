using Application.Abstractions.Storage;
using MediatR;

namespace Application.Media.Queries.GetUsersAvatars
{
    public sealed record GetUsersAvatarsQuery(
        int UserId,
        string ImageId
    ):IRequest<FileResponse>;
}
