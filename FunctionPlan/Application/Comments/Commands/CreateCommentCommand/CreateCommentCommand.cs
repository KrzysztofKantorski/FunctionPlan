using Application.Abstractions.Messaging;

namespace Application.Comments.Commands.CreateCommentCommand
{
    public sealed record CreateCommentCommand(
        int MeetingId,
        int AuthorId,
        string Content,
        int? ParentCommentId
    ):ICommand;
}
