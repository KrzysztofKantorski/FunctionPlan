using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Common.Helpers;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Meetings.Queries.GetMyMeetings
{
    internal sealed class GetMyMeetingsQueryHandler: IRequestHandler<GetMyMeetingsQuery, List<MeetingListDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMyMeetingsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingListDto>> Handle(GetMyMeetingsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                 """
                    SELECT m."Id", m."Title", m."ScheduledFor", m."OrganizerId", org."Username" AS "OrganizerName"
                    FROM "Meetings" m
                    INNER JOIN "MeetingUser" mu ON m."Id" = mu."MeetingsId"
                    INNER JOIN "Users" org ON m."OrganizerId" = org."Id"
                """;

            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            //User must be meeting participant
            conditions.Add("mu.\"UsersId\" = @userId");
            parameters.Add("userId", request.userId);

            conditions.AddDateRangeFilter(parameters, "m.\"ScheduledFor\"", request.Filters.StartDate, request.Filters.EndDate);
            conditions.ApplySearchTerm(parameters, "m.\"Title\"", request.Filters.SearchTerm);

            sql = SqlExtensions.ApplyWhereConditions(sql, conditions);
            sql = SqlExtensions.ApplySorting(sql, "m.\"ScheduledFor\"", request.Filters.SortOrder);

            var myMeetings = await connection.QueryAsync<MeetingListDto>(sql, parameters);

            return myMeetings.ToList();
        } 
    }
}
