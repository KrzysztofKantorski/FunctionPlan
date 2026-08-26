using Application.Abstractions.Messaging;

namespace Application.Media.Commands.AddMediaFile
{
    public sealed record AddMediaFileCommand(
        int UploaderId,
        int MeetingId,
        string? Description,
        FileDto File
    ) :ICommand;
}
