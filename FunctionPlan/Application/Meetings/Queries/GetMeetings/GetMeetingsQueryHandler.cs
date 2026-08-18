using Application.Abstractions.Data;
using Dapper;
using Domain.Meetings;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetMeetings
{
    internal sealed class GetMeetingsQueryHandler : IRequestHandler<GetMeetingsQuery, List<MeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingListDto>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql = """
                SELECT "Id", "Title", "ScheduledFor", "OrganizerId"
                FROM "Meetings"
                """;

            //Values from query parameters
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            //Get meetings in progress and planned
            conditions.Add("m.\"Status\" IN (@Planned, @InProgress)");
            parameters.Add("Planned", (int)MeetingStatus.Planned);
            parameters.Add("InProgress", (int)MeetingStatus.InProgress);


            //Get meetings with proper date
            conditions.Add("m.\"ScheduledFor\" > @Now");
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
