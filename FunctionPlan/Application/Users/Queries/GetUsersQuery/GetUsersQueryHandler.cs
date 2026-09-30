using Application.Abstractions.Data;
using Application.Common.Dto;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Users.Queries.GetUsersQuery
{
    internal sealed class GetUsersQueryHandler: IRequestHandler<GetUsersQuery, List<UserProfileDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUsersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<UserProfileDetailsDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                """
                    SELECT u."Id", u."Username", u."Email", u."ProfilePictureUrl"
                    FROM "Users" u WHERE u."Id" != @UserId AND u."IsBanned" = false
                """;

                var users = await connection.QueryAsync<UserProfileDetailsDto>(
                   sql,
                   new { request.UserId }
                );

                return users.ToList();
        }
    }
}
