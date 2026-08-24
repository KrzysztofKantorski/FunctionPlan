namespace Application.Abstractions.Storage
{
    public interface IBlobService
    {
        Task UploadFileAsync(Guid fileId, Stream stream, string contentType, CancellationToken cancellationToken);   

        Task<FileResponse> DownloadFileAsync(Guid fileId, CancellationToken cancellationToken);

        Task DeleteFileAsync(Guid fileId, CancellationToken cancellationToken);
    }
}
