using Application.Abstractions.Data;
using Application.Abstractions.Storage;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Media.Queries.GetUsersAvatars
{
    internal sealed class GetUsersAvatarsQueryHandler : IRequestHandler<GetUsersAvatarsQuery, FileResponse>
    {
        private readonly IBlobService _blobService;
        private readonly BlobSettings _blobSettings;

        public GetUsersAvatarsQueryHandler( IBlobService blobService, IOptions<BlobSettings> blobOptions)
        {
            _blobService = blobService;
            _blobSettings = blobOptions.Value;
        }

        public async Task<FileResponse> Handle(GetUsersAvatarsQuery request, CancellationToken cancellationToken)
        {
            //Format GUID
            string cleanAvatarId = request.ImageId.Trim();

            if (!Guid.TryParse(cleanAvatarId, out var avatarGuid))
            {
                throw new Exception("Invalid AvatarId format. It must be a valid GUID.");
            }

            //Get file from cloud
            var file = await _blobService.DownloadFileAsync(
                _blobSettings.AvatarsContainerName,
                avatarGuid,
                cancellationToken);

            return file;

        }
    }
}
