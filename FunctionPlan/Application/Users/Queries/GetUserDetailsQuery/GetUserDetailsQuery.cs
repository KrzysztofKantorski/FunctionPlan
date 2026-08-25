using Application.Abstractions.Messaging;

namespace Application.Users.Queries.GetUserDetailsQuery
{
    public sealed record GetUserDetailsQuery(
        int UserId
    ): ICommand<UserProfileDetailsDto>;
}
