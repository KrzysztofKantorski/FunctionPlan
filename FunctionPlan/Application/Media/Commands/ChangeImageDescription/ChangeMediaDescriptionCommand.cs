using Application.Abstractions.Messaging;

namespace Application.Media.Commands.ChangeImageDescription
{
    public sealed record ChangeMediaDescriptionCommand
    (
        int MeetingId,
        int UserId,
        string ImageId, 
        string Description
    ):ICommand;
}
