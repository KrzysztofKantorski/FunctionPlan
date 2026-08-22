using Application.Abstractions.Messaging;

namespace Application.Users.Queries
{
    public sealed record GetUserDetailsQuery(
        int UserId
    ): ICommand<UserProfileDetailsDto>;
}
