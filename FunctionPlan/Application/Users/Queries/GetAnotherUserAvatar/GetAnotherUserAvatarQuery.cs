using Application.Abstractions.Storage;
using MediatR;

namespace Application.Users.Queries.GetAnotherUserAvatar
{
    public sealed record GetAnotherUserAvatarQuery
    (
        int userId
    ):IRequest<FileResponse?>;
}
