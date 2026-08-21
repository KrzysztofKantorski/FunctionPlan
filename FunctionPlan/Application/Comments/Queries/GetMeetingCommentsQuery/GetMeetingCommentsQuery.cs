using Application.Abstractions.Messaging;


namespace Application.Comments.Queries.GetMeetingCommentsQuery
{
    public sealed record GetMeetingCommentsQuery(
        int MeetingId
    ):ICommand<List<CommentDto>>;
}
