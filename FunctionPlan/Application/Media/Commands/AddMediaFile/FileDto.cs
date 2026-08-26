namespace Application.Media.Commands.AddMediaFile
{
    public sealed record FileDto
    (
       Stream Stream,
       string FileName,
       string ContentType
    );
}
