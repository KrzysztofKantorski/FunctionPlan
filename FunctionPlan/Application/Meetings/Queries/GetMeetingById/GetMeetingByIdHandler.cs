using Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;
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
                        m."Id", m."Title", m."Description", m."ScheduledFor", m."Latitude", m."Longitude",
                        u."Id", u."Username"
                    FROM "Meetings" m
                    INNER JOIN "Users" u ON m."OrganizerId" = u."Id"
                    WHERE m."Id" = @MeetingId;

                    SELECT 
                        u."Id", 
                        u."Username",
                        u."ProfilePictureUrl"
                    FROM "Users" u
                    INNER JOIN "MeetingUser" mu ON u."Id" = mu."UsersId"
                    WHERE mu."MeetingsId" = @MeetingId;
                """;

            //Execute queries
            using var multi = await connection.QueryMultipleAsync(sql, new { request.MeetingId });

            //Map first query
            var meeting = multi.Read<MeetingDto, OrganizerDto, MeetingDto>(
                (m, organizer) =>
                {
                    m.Organizer = organizer;
                    return m;
                },
                splitOn: "Id"
            ).FirstOrDefault();

            if (meeting is null)
            {
                return null;
            }

            //Map meeting attendees
            var attendees = (await multi.ReadAsync<AttendeeDto>()).ToList();
            meeting.Attendees = attendees;


            //Return one meeting object
            return meeting;
        }
    }
}
