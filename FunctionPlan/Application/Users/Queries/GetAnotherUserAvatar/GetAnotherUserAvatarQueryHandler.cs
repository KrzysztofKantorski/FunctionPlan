using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Application.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;

namespace Application.Users.Queries.GetAnotherUserAvatar
{
    internal sealed class GetAnotherUserAvatarQueryHandler : IRequestHandler<GetAnotherUserAvatarQuery, FileResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;

        public GetAnotherUserAvatarQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobService blobService,
           IOptions<BlobSettings> blobOptions)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<FileResponse> Handle(GetAnotherUserAvatarQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql =
                """
                    SELECT u."ProfilePictureUrl" from "Users" u 
                    WHERE u."Id" = @userId
                """;

            //Query user image
            var imageId = await connection.QueryFirstOrDefaultAsync<string>(
                 sql,
                 new { request.userId }
            );

            //Check if image exists
            if (string.IsNullOrWhiteSpace(imageId) || !Guid.TryParse(imageId, out var fileId))
            {
                return null;
            }

            //Get file from azure blob
            var file = await _blobService.DownloadFileAsync(_blobSettings.AvatarsContainerName, fileId, cancellationToken);

            return file;
        }
    }
}
