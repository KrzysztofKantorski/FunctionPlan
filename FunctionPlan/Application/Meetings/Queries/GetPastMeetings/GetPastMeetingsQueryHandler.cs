using Application.Abstractions.Data;
using Dapper;
using Domain.Meetings;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetPastMeetings
{
    internal sealed class GetPastMeetingsQueryHandler : IRequestHandler<GetPastMeetingsQuery, List<MeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPastMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingListDto>> Handle(GetPastMeetingsQuery request, CancellationToken cancellationToken)
        {

            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql = """
                SELECT m."Id", m."Title", m."ScheduledFor", m."OrganizerId", u."Username" AS "OrganizerName"
                FROM "Meetings" m
                INNER JOIN "Users" u ON m."OrganizerId" = u."Id"
                """;

            //Values from query parameters
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            //Get cancelled and completed meetings
            conditions.Add("m.\"Status\" IN (@Cancelled, @Completed)");
            parameters.Add("Cancelled", (int)MeetingStatus.Planned);
            parameters.Add("Completed", (int)MeetingStatus.InProgress);

            //Get meetings with proper date
            conditions.Add("m.\"ScheduledFor\" < @Now");
            parameters.Add("Now", DateTime.UtcNow);

            //Search by meeting title
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                conditions.Add("\"Title\" ILIKE @SearchTerm");
                parameters.Add("SearchTerm", $"%{request.SearchTerm}%");
            }

            //Add conditions
            if (conditions.Any())
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }

            //Sorting type
            var sortDirection = request.SortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
            sql += $"\nORDER BY \"ScheduledFor\" {sortDirection}";

            var meetings = await connection.QueryAsync<MeetingListDto>(sql, parameters);
            return meetings.ToList();
        }
    }
}
