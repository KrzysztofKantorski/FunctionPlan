using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Meetings.Queries.GetMeetingAttendeesQuery;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetMyMeetings
{
    internal sealed class GetMyMeetingsQueryHandler: IRequestHandler<GetMyMeetingsQuery, List<MeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMyMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingListDto>> Handle(GetMyMeetingsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                 """
                    SELECT u."Id", u."Username"
                    FROM "Users" u
                    INNER JOIN "MeetingUser" mu ON u."Id" = mu."UsersId"
                    WHERE u."Id" = @userId
                """;

            var myMeetings = await connection.QueryAsync<MeetingListDto>(
                sql,
                new { request.userId }
            );

            return myMeetings.ToList();
        }
    }
}
