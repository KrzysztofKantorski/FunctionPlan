using Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Users.Queries.GetUserStats
{
    internal class GetUserStatsQueryHandler: IRequestHandler<GetUserStatsQuery, UserStatsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetUserStatsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserStatsDto> Handle(GetUserStatsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();


            var sql =
               """
                SELECT
                   (SELECT COUNT(*) FROM "Meetings" WHERE "OrganizerId" = @UserId) AS OrganizedMeetingsCount),   
                   (SELECT COUNT(*) FROM "MeetingUser" WHERE "UsersId" = @UserId) AS AttendedMeetingsCount),  
                   (SELECT COUNT(*) FROM "Comments" WHERE "AuthorId" = @UserId) AS CommentsCount),   
                   (SELECT COUNT(*) FROM "MediaFiles" WHERE "UploaderId" = @UserId) AS UploadedPhotosCount)
                """;

            return await connection.QuerySingleAsync<UserStatsDto>(sql, new { request.UserId });
        }
    }
}
