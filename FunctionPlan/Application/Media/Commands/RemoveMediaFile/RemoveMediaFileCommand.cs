using Application.Abstractions.Messaging;

namespace Application.Media.Commands.RemoveMediaFile
{
    public sealed record RemoveMediaFileCommand
    (
        int MeetingId,
        int UserId,
        string ImageId
    ):ICommand;
}
