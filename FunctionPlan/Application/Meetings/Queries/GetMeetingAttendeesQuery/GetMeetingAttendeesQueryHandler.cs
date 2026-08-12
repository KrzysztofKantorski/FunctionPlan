using Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetMeetingAttendeesQuery
{
    internal sealed class GetMeetingAttendeesQueryHandler : IRequestHandler<GetMeetingAttendeesQuery, List<AttendeeDto>>
    {

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingAttendeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<AttendeeDto>> Handle(GetMeetingAttendeesQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql = 
                """
                    SELECT u."Username", u."Id",
                    FROM "Users" u
                    INNER JOIN "MeetingUser" mu ON u."Id" = mu."UsersId"
                    WHERE mu."MeetingsId" = @MeetingId
                """;
            var attendees = await connection.QueryAsync<AttendeeDto>(
                sql,
                new { request.MeetingId }
            );

            return attendees.ToList();
        }
    }

}
