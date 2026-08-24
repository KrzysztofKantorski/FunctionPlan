namespace Application.Abstractions.Image
{
    public interface IImageProcessor
    {
        Task<Stream> ProcessImageAsync(Stream originalStream, CancellationToken cancellationToken);
    }
}
