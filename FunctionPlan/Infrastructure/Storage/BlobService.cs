using Application.Abstractions.Storage;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Infrastructure.Storage
{
    internal sealed class BlobService(BlobServiceClient blobServiceClient) : IBlobService
    {
        public async Task UploadFileAsync(Guid fileId, Stream stream, string contentType, CancellationToken cancellationToken)
        {
           
            if (stream.CanSeek)
            {
                stream.Position = 0;
            }


            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient();

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders { ContentType = contentType }
               
            };

            await blobClient.UploadAsync(stream, options, cancellationToken);
        }



        public async Task<FileResponse> DownloadFileAsync(Guid fileId, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient();

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            Response<BlobDownloadStreamingResult> response = await blobClient.DownloadStreamingAsync(cancellationToken: cancellationToken);

            return new FileResponse(
                response.Value.Content,
                response.Value.Details.ContentType
            );
        }



        public async Task DeleteFileAsync(Guid fileId, CancellationToken cancellationToken)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient();

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }



    }
}
