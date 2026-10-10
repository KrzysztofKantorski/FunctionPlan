using Application.Abstractions.Data;
using Application.Exceptions;
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
                   u."Id" AS "Id",
                   u."Username" AS "Username",
                   (SELECT COUNT(*) FROM "Meetings" WHERE "OrganizerId" = @UserId) AS OrganizedMeetingsCount,   
                   (SELECT COUNT(*) FROM "MeetingUser" WHERE "UsersId" = @UserId) AS AttendedMeetingsCount,  
                   (SELECT COUNT(*) FROM "Comments" WHERE "AuthorId" = @UserId) AS CommentsCount,   
                   (SELECT COUNT(*) FROM "MediaFiles" WHERE "UploaderId" = @UserId) AS UploadedPhotosCount

                   FROM "Users" u
                   WHERE u."Id" = @UserId
                """;

            var stats = await connection.QuerySingleOrDefaultAsync<UserStatsDto>(sql, new { request.UserId });

            if(stats == null)
            {
                throw new UserNotFoundException("User with provided id not found");
            }

            return stats;
        }
    }
}
