using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using Application.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Options;
using System.Data;

namespace Application.Users.Queries.GetUserImageQuery
{
    internal sealed class GetUserImageQueryHandler : IRequestHandler<GetUserImageQuery, FileResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;
        public GetUserImageQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IBlobService blobService,
            IOptions<BlobSettings> blobOptions)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<FileResponse> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();

            var sql = 
                """
                    SELECT u."ProfilePictureUrl" from "Users" u 
                    WHERE u."Id" = @UserId
                """;

            //Query user image
            var imageId = await connection.QueryFirstOrDefaultAsync<string>(
                 sql,
                 new { request.UserId }
            );

            //Check if image exists
            if(string.IsNullOrWhiteSpace(imageId) || !Guid.TryParse(imageId, out var fileId))
            {
                throw new ImageNotFound("User image not found");
            }

            //Get file from azure blob
            var file = await _blobService.DownloadFileAsync(_blobSettings.AvatarsContainerName, fileId, cancellationToken);

            return file;
        }
    }
}
