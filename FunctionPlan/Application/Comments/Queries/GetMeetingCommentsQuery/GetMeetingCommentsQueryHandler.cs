using Application.Abstractions.Data;
using Dapper;
using MediatR;
using System.Data;

namespace Application.Comments.Queries.GetMeetingCommentsQuery
{
    internal sealed class GetMeetingCommentsQueryHandler: IRequestHandler<GetMeetingCommentsQuery, List<CommentDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMeetingCommentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<CommentDto>> Handle(GetMeetingCommentsQuery request, CancellationToken cancellationToken)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateDbConnection();


            var sql = """

                SELECT
                    c."Id", 
                    c."Content",
                    c."CreatedAt", 
                    c."ParentCommentId",
                    u."Username" 
                    FROM "Comments" c
                    INNER JOIN "Users" u ON c."AuthorId" = u."Id"
                    WHERE c."MeetingId" = @MeetingId
                        AND c."IsHidden" = FALSE
                    ORDER BY c."CreatedAt" ASC
                """;

            var flatComments = await connection.QueryAsync<CommentDto>(
                sql,
                new { MeetingId = request.MeetingId }
            );

            var commentsList = flatComments.ToList();
            var commentsById = commentsList.ToDictionary(c => c.Id);
            var rootComments = new List<CommentDto>();


            foreach (var comment in commentsList) 
            {
                //Check if comment has replies
                if(comment.ParentCommentId.HasValue)
                {
                    if (commentsById.TryGetValue(comment.ParentCommentId.Value, out var parent))
                    {
                        parent.Replies.Add(comment);
                    }
                }
                else
                {
                    rootComments.Add(comment);
                }
            }

            return rootComments;
        }
    }
}
