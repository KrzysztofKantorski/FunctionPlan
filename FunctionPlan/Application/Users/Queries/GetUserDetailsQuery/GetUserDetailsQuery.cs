using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Users.Queries.GetUserDetailsQuery
{
    public sealed record GetUserDetailsQuery(
        int UserId
    ): ICommand<UserProfileDetailsDto>;
}
