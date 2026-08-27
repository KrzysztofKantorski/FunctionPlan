using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Application.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;
using System.IO;
using static System.Net.WebRequestMethods;


namespace Application.Media.Queries.GetUsersAvatars
{
    internal sealed class GetUsersAvatarsQueryHandler : IRequestHandler<GetUsersAvatarsQuery, FileResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;

        public GetUsersAvatarsQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobService blobService,
             IOptions<BlobSettings> blobOptions)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<FileResponse> Handle(GetUsersAvatarsQuery request, CancellationToken cancellationToken)
        {
            //What we need
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            //Get meeting attendees
            var sql = 
                """
                SELECT 
                    m."Status", 
                    m."OrganizerId", 
                    (mu."UsersId" IS NOT NULL) AS "IsParticipant"
                FROM "Meetings" m 
                LEFT JOIN "MeetingUser" mu ON m."Id" = mu."MeetingsId" AND mu."UsersId" = @UserId
                WHERE m."Id" = @MeetingId
                """;

            var MeetingAttendees = await connection.QueryFirstOrDefaultAsync<MeetingAuthDto>(
               sql,
               new { request.MeetingId, request.UserId }
            );


            if (MeetingAttendees is null)
            {
                throw new Exception("Incorrect meeting id.");
            }

            //Check if meeting was cancelled
            if ((int)MeetingAttendees.Status == 3)
            {
                throw new Exception("Cannot access cancelled meeting.");
            }

            //Check if user belongs to meeting
            if ((int)MeetingAttendees.OrganizerId != request.UserId && !MeetingAttendees.IsParticipant)
            {
                throw new UserWithoutPermisionException("You don't have access to this meeting's media.");
            }


            //Get user avatar 
            var avatar = 
                """
                SELECT 
                    u."ProfilePictureUrl",
                    u."UserName"
                FROM "Users" u 
                WHERE u."Id" = @UserId
                """;

            var userImage = await connection.QueryFirstOrDefaultAsync<UserAvatarDto>(
              avatar,
              new { request.UserId }
            );

            if (userImage.ProfilePictureUrl is null)
            {
                throw new Exception("User image not found");
            }

            string imageIdString = request.ImageId.ToString().Trim();

            if (!Guid.TryParse(imageIdString, out var avatarGuid))
            {
                throw new Exception("Invalid ImageId format. It must be a valid GUID.");
            }

            //Get file from azure blob
            var file = await _blobService.DownloadFileAsync(
                _blobSettings.MeetingsContainerName, 
                avatarGuid, 
                cancellationToken
            );

            return file;

        }
    }
}
