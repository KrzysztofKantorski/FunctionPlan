using Application.Abstractions.Messaging;

namespace Application.Users.Commands.UploadUserImage
{
    public sealed record UploadUserImageCommand
    (
        FileDto UploadedImage,
        int UserId
    ):ICommand;
}
