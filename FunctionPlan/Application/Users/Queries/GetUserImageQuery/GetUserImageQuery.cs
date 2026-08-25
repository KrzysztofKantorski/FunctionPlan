using Application.Abstractions.Storage;
using MediatR;

namespace Application.Users.Queries.GetUserImageQuery
{
    public sealed record GetUserImageQuery(
        int UserId
    ): IRequest<FileResponse>;
}
