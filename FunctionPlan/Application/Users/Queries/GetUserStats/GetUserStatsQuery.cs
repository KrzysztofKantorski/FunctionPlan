using MediatR;

namespace Application.Users.Queries.GetUserStats
{
    public sealed record GetUserStatsQuery(
        int UserId): IRequest<UserStatsDto>;
}
