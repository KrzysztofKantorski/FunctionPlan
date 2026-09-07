using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Meetings.Queries.GetAttendeedMeetings;
using Application.Meetings.Queries.GetMyMeetings;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetAttendedMeetings
{
    internal sealed class GetOrganizedMeetingsQueryHandler: IRequestHandler<GetOrganizedMeetingsQuery, List<MeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetOrganizedMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<MeetingListDto>> Handle(GetOrganizedMeetingsQuery request, CancellationToken cancellationToken)
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
