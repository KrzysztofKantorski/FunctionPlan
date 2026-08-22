using Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Users.Queries
{
    internal sealed class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserProfileDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<UserProfileDetailsDto?> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql = """
                SELECT "Id", "Username", "Email", "ProfilePictureUrl"
                FROM Users WHERE Users."Id" = @UserId
                """;

            var userDetails = await connection.QueryAsync<UserProfileDetailsDto>(
                 sql,
                 new { request.UserId }
            );

            return userDetails.FirstOrDefault();
        }
    }
}
