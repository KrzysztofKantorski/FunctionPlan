using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Meetings.Queries.GetMyMeetings;
using Dapper;
using System.Data;

namespace Application.Meetings.Queries.GetAttendedMeetings
{
    internal sealed class GetAttendedMeetingsQueryHandler
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAttendedMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingListDto>> Handle(GetMyMeetingsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                 """
                    SELECT m."Id", m."Title", m."ScheduledFor", m."OrganizerId", org."Username" AS "OrganizerName"
                    FROM "Meetings" m
                    INNER JOIN "Users" org ON m."OrganizerId" = org."Id"
                    WHERE m."OrganizerId" = @userId
                    ORDER BY m."ScheduledFor" ASC
                """;

            var myMeetings = await connection.QueryAsync<MeetingListDto>(
                sql,
                new { request.userId }
            );

            return myMeetings.ToList();
        }
    }
}
