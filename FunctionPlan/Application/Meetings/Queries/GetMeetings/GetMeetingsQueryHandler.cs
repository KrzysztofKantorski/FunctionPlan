using Application.Abstractions.Data;
using Application.Common.Dto;
using Application.Common.Helpers;
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
                SELECT m."Id", m."Title", m."Description", m."ScheduledFor", m."OrganizerId", u."Username" AS "OrganizerName"
                FROM "Meetings" m
                INNER JOIN "Users" u ON m."OrganizerId" = u."Id"
                """;


            //Values from query parameters
            var conditions = new List<string>();
            var parameters = new DynamicParameters();

            //Display only meetings avaliable to join
            conditions.Add("m.\"OrganizerId\" != @UserId");

            //Dont display meetings that user alerdy joined
            conditions.Add("""
                NOT EXISTS (
                    SELECT 1 FROM "MeetingUser" mu 
                    WHERE mu."MeetingsId" = m."Id" AND mu."UsersId" = @UserId
                )
                """);

            parameters.Add("UserId", request.UserId);
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

            conditions.AddDateRangeFilter(parameters, "m.\"ScheduledFor\"", request.StartDate, request.EndDate);
            conditions.ApplySearchTerm(parameters, "m.\"Title\"", request.SearchTerm);

            sql = SqlExtensions.ApplyWhereConditions(sql, conditions);
            sql = SqlExtensions.ApplySorting(sql, "m.\"ScheduledFor\"", request.SortOrder);

            var meetings = await connection.QueryAsync<MeetingListDto>(sql, parameters);
            return meetings.ToList();
        }
    }
    
}
