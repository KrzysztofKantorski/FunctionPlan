
using MediatR;


namespace Application.Comments.Queries.GetMeetingCommentsQuery
{
    public sealed record GetMeetingCommentsQuery(
        int MeetingId
    ):IRequest<List<CommentDto>>;
}
