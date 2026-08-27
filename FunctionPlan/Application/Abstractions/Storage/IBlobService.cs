namespace Application.Abstractions.Storage
{
    public interface IBlobService
    {
        Task UploadFileAsync(string containerName, Guid fileId, Stream stream, string contentType, CancellationToken cancellationToken);   

        Task<FileResponse> DownloadFileAsync(string containerName, Guid fileId, CancellationToken cancellationToken);

        Task DeleteFileAsync(string containerName, Guid fileId, CancellationToken cancellationToken);
    }
}
