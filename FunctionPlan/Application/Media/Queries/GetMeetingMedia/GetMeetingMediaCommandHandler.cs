using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Application.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;

namespace Application.Media.Queries.GetMeetingMedia
{
    internal sealed class GetMeetingMediaCommandHandler : IRequestHandler<GetMeetingMediaCommand, List<MeetingMediaDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingMediaCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<MeetingMediaDto>> Handle(GetMeetingMediaCommand request, CancellationToken cancellationToken)
        {

            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            //Get meeting attendees
            var authSql =
                """
                SELECT
                    m."Status", m."OrganizerId", (mu."UsersId" IS NOT NULL) AS IsParticipant
                FROM "Meetings" m
                    LEFT JOIN "MeetingUser" mu ON m."Id" = mu."MeetingsId" AND mu."UsersId" = @UserId
                WHERE m."Id" = @MeetingId
                """;

            var meetingInfo = await connection.QueryFirstOrDefaultAsync<MeetingAuthInfo>(
                authSql, 
                new 
                { 
                    request.MeetingId, 
                    request.UserId 
                }
            );

            if(meetingInfo is null)
            {
                throw new MeetingNotFoundException("Incorrect meeting");
            }


            //Check if meeting is cancelled
            if((int)meetingInfo.Status == 3)
            {
                throw new Exception("Cannot view media of a cancelled meeting.");
            }

            //Check if user belongs to meeting
            if((int)meetingInfo.OrganizerId != request.UserId && meetingInfo.IsParticipant)
            {
                throw new UserWithoutPermisionException("You don't have access to this meeting's media.");
            }


            //Get media info
            var mediaSql = 
             """
                SELECT 
                    md."FileName", 
                    md."Description", 
                    md."CreatedAt",
                    u."Username" AS "UploaderName",
                    u."ProfilePictureUrl" AS "UploaderAvatarId"
                FROM "Media" md
                INNER JOIN "Users" u ON md."UploaderId" = u."Id"
                WHERE md."MeetingId" = @MeetingId
                ORDER BY md."CreatedAt" DESC
             """;

            var mediaFiles = await connection.QueryAsync<MeetingMediaDto>(mediaSql, new { request.MeetingId });

            return mediaFiles.ToList();
        }

    }
}
