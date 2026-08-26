namespace Application.Common.Dto
{
    public sealed record FileDto
    (
       Stream Stream,
       string FileName,
       string ContentType
    );
}
