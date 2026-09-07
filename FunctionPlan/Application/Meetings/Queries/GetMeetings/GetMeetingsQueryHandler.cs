using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Exceptions;
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
                SELECT m."Id", m."Title", m."ScheduledFor", m."OrganizerId", u."Username" AS "OrganizerName"
                FROM "Meetings" m
                INNER JOIN "Users" u ON m."OrganizerId" = u."Id"
                """;


            //Values from query parameters
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            //Get allowed meeting statuses
            var allowedStatuses = new List<int> { (int)MeetingStatus.Planned, (int)MeetingStatus.InProgress };

            //Check if user provided proper date range
            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate > request.EndDate)
            {
                throw new InvalidRequestData("StartDate cannot be later than EndDate.");
            }

            if (request.Status.HasValue)
            {
                if (allowedStatuses.Contains(request.Status.Value))
                {
                    conditions.Add("m.\"Status\" = @RequestedStatus");
                    parameters.Add("RequestedStatus", request.Status.Value);
                }
                else
                {
                    throw new Exception("Incorrect meeting status");
                }
            }
            else
            {
                //Get meetings in progress and planned
                conditions.Add("m.\"Status\" IN (@Planned, @InProgress)");
                parameters.Add("Planned", (int)MeetingStatus.Planned);
                parameters.Add("InProgress", (int)MeetingStatus.InProgress);
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
