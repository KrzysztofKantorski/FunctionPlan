namespace Application.Comments.Queries.GetMeetingCommentsQuery
{
    public sealed class CommentDto
    {
        public int Id { get; init; }
        public string Username { get; init; }
        public string Content { get; init; }
        public DateTime CreatedAt { get; init; }
        public int? ParentCommentId { get; init; }
        public List<CommentDto> Replies { get; set; } = new();
    }
}
