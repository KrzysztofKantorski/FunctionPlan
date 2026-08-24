using Application.Abstractions.Messaging;

namespace Application.Users.Commands.UploadUserImage
{
    public sealed record UploadUserImageCommand
    (
        Stream UploadedImage,
        int UserId
    ):ICommand;
}
