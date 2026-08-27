using Application.Abstractions.Storage;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Storage
{
    internal sealed class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient, IOptions<BlobSettings> options)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task UploadFileAsync(string containerName, Guid fileId, Stream stream, string contentType, CancellationToken cancellationToken)
        {
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }

            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }

            };

            await blobClient.UploadAsync(stream, options, cancellationToken);
        }

        public async Task<FileResponse> DownloadFileAsync(string containerName, Guid fileId, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            Response<BlobDownloadStreamingResult> response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

            return new FileResponse(
                response.Value.Content,
                response.Value.Details.ContentType
            );
        }

        public async Task DeleteFileAsync(string containerName, Guid fileId, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
    }
}
