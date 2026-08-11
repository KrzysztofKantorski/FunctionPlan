using Application.Abstractions.Data;
using MediatR;
using System.Data;
using Dapper;
namespace Application.Meetings.Queries.GetMeetingById
{
    internal sealed class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdQuery, MeetingDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingByIdHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<MeetingDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                """
                    SELECT 
                    m."Id", m."Title", m."ScheduledFor", m."Latitude", m."Longitude",
                    u."Id", u."Name"
                    FROM "Meetings" m
                    INNER JOIN "Users" u ON m."OrganizerId" = u."Id"
                    WHERE m."Id" == @MeetingId
                """;

            return await connection.QueryFirstOrDefaultAsync<MeetingDto>(sql, new { request.MeetingId });
        }
    }
}
