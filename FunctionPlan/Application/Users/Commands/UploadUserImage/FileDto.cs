namespace Application.Users.Commands.UploadUserImage
{
    public sealed record FileDto
    (
        Stream Stream,
        string Filename,
        string ContentType
    );
}
