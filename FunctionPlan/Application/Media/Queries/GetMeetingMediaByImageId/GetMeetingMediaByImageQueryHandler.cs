using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Application.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;

namespace Application.Media.Queries.GetMeetingMediaByImageId
{
    internal sealed class GetMeetingMediaByImageQueryHandler : IRequestHandler<GetMeetingMediaByImageQuery, FileResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;

        public GetMeetingMediaByImageQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobService blobService,
             IOptions<BlobSettings> blobOptions)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<FileResponse> Handle(GetMeetingMediaByImageQuery request, CancellationToken cancellationToken)
        {

            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            //Get meeting attendees
            var sql = """
                SELECT 
                    md."FileName",
                    m."Status", 
                    m."OrganizerId", 
                    (mu."UsersId" IS NOT NULL) AS IsParticipant
                FROM "MediaFiles" md
                INNER JOIN "Meetings" m ON md."MeetingId" = m."Id"
                LEFT JOIN "MeetingUser" mu ON m."Id" = mu."MeetingsId" AND mu."UsersId" = @UserId
                WHERE md."FileName" = @FileName AND m."Id" = @MeetingId
                """;

            var fileInfo = await connection.QueryFirstOrDefaultAsync<dynamic>(
                sql,
                new { request.ImageId, request.MeetingId, request.UserId }
            );

            if (fileInfo is null)
            {
                throw new Exception("File not found or does not belong to this meeting.");
            }

            //Check if meeting was cancelled
            if ((int)fileInfo.Status == 3) 
            {
                throw new Exception("Cannot view media of a cancelled meeting.");
            }

            //Check if user belongs to meeting
            if ((int)fileInfo.OrganizerId != request.UserId && !fileInfo.IsParticipant)
            {
                throw new UserWithoutPermisionException("You don't have access to this meeting's media.");
            }


            string imageId = fileInfo.ImageId;


            //Check if image exists
            if (string.IsNullOrWhiteSpace(imageId) || !Guid.TryParse(imageId, out var fileId))
            {
                throw new ImageNotFound("User image not found");
            }


            //Get file from azure blob
            var file = await _blobService.DownloadFileAsync(_blobSettings.AvatarsContainerName, fileId, cancellationToken);

            return file;
        }
    }
}
