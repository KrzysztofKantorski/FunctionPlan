using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Media.Commands.AddMediaFile
{
    public sealed record AddMediaFileCommand(
        int UploaderId,
        int MeetingId,
        string? Description,
        FileDto File
    ) :ICommand;
}
