using Application.Abstractions.Data;
using Application.Common.Helpers;
using Application.Exceptions;
using Application.Meetings.Queries.GetAttendeedMeetings;
using Application.Meetings.Queries.GetOrganizedMeetings;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetAttendedMeetings
{
    internal sealed class GetOrganizedMeetingsQueryHandler: IRequestHandler<GetOrganizedMeetingsQuery, List<OrganizedMeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetOrganizedMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<OrganizedMeetingListDto>> Handle(GetOrganizedMeetingsQuery request, CancellationToken cancellationToken)
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

            conditions.AddDateRangeFilter(parameters, "m.\"ScheduledFor\"", request.Filters.StartDate, request.Filters.EndDate);
            conditions.ApplySearchTerm(parameters, "m.\"Title\"", request.Filters.SearchTerm);

            sql = SqlExtensions.ApplyWhereConditions(sql, conditions);
            sql = SqlExtensions.ApplySorting(sql, "m.\"ScheduledFor\"", request.Filters.SortOrder);


            var meetings = await connection.QueryAsync<OrganizedMeetingListDto>(sql, parameters);
            return meetings.ToList();
        }
    }
}
