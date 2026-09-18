using Application.Abstractions.Data;
using Application.Exceptions;
using Application.Meetings.Queries.GetAttendeedMeetings;
using Application.Meetings.Queries.GetOrganizedMeetings;
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
                """;

            //Values from query parameters
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            conditions.Add("m.\"OrganizerId\" = @userId");
            parameters.Add("userId", request.userId);

            //Check if user provided proper date range
            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate > request.EndDate)
            {
                throw new InvalidRequestData("StartDate cannot be later than EndDate.");
            }

            if (request.StartDate.HasValue)
            {
                conditions.Add("m.\"ScheduledFor\" >= @StartDate");
                parameters.Add("StartDate", request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                conditions.Add("m.\"ScheduledFor\" <= @EndDate");
                parameters.Add("EndDate", request.EndDate.Value.Date.AddDays(1).AddTicks(-1));
            }

            //Search by meeting title
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                conditions.Add("m.\"Title\" ILIKE @SearchTerm");
                parameters.Add("SearchTerm", $"%{request.SearchTerm}%");
            }

            //Add conditions
            if (conditions.Any())
            {
                sql += " WHERE " + string.Join(" AND ", conditions);
            }

            //Sorting type
            var sortDirection = request.SortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
            sql += $"\nORDER BY m.\"ScheduledFor\" {sortDirection}";


            var meetings = await connection.QueryAsync<MeetingListDto>(sql, parameters);
            return meetings.ToList();
        }
    }
}
