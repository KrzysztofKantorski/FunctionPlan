using Application.Abstractions.Messaging;
using Application.Common.Dto;
namespace Application.Users.Queries.GetUsersQuery
{
    public sealed record GetUsersQuery(
        int UserId
    ): ICommand<List<UserProfileDetailsDto>>;
}
