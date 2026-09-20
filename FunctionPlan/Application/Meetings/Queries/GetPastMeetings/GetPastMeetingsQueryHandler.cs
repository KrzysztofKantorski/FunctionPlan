using Application.Abstractions.Data;
using Application.Common.Helpers;
using Application.Exceptions;
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

            //Get allowed meeting statuses
            var allowedStatuses = new List<int> { (int)MeetingStatus.Completed, (int)MeetingStatus.Cancelled };

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
                //Get cancelled and completed meetings
                conditions.Add("m.\"Status\" IN (@Cancelled, @Completed)");
                parameters.Add("Cancelled", (int)MeetingStatus.Cancelled);
                parameters.Add("Completed", (int)MeetingStatus.Completed);
            }

            //Apply filters, conditions
            conditions.AddDateRangeFilter(parameters, "m.\"ScheduledFor\"", request.StartDate, request.EndDate);
            conditions.ApplySearchTerm(parameters, "m.\"Title\"", request.SearchTerm);

            sql = SqlExtensions.ApplyWhereConditions(sql, conditions);
            sql = SqlExtensions.ApplySorting(sql, "m.\"ScheduledFor\"", request.SortOrder);

            var meetings = await connection.QueryAsync<MeetingListDto>(sql, parameters);
            return meetings.ToList();
        }
    }
}
