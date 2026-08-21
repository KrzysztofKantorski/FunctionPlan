namespace Application.Comments.Commands.CreateCommentCommand
{
    public sealed record CreateCommentUserRequest(
        string Content,
        int? ParentCommentId
    );
}
