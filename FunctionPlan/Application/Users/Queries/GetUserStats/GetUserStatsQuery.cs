using MediatR;

namespace Application.Users.Queries.GetUserStats
{
    internal sealed record GetUserStatsQuery(
        int UserId): IRequest<UserStatsDto>;
}
