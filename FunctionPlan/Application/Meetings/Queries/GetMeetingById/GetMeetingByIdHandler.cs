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
                SELECT "Id", "Title", "ScheduledFor", "Latitude", "Longitude" FROM "Meetings" WHERE  "Id" = @MeetingId
                """;

            return await connection.QueryFirstOrDefaultAsync<MeetingDto>(sql, new { request.MeetingId });
        }
    }
}
