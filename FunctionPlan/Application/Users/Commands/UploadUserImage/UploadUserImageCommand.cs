using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Users.Commands.UploadUserImage
{
    public sealed record UploadUserImageCommand
    (
        FileDto UploadedImage,
        int UserId
    ):ICommand;
}
