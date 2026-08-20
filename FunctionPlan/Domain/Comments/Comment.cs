using Domain.Common;
using Domain.Meetings;
using Domain.Users;

namespace Domain.Comments
{
    public sealed class Comment: Entity
    {
        public int MeetingId { get; private set; }
        public Meeting Meeting { get; private set; }
        public int AuthorId { get; private set; }
        public User Author { get; private set; }
        public string Content { get; private set; }
        public int? ParentCommentId { get; private set; }
        public Comment? ParentComment { get; private set; }
        public DateTime CreatedAt {  get; private set; }
        public bool IsHidden { get; private set; }

        private readonly List<Comment> _replies = new();
        public IReadOnlyCollection<Comment> Replies => _replies.AsReadOnly();

        private Comment() { }

        public Comment(int meetingId, int authorId, string content, int? parentCommentId=null)
        {
            if(meetingId < 0)
            {
                throw new Exception("Incorrect meeting");
            }

            if (authorId < 0)
            {
                throw new Exception("Incorrect autho");
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new Exception("omment cannot be empty");
            }

          
            MeetingId = meetingId;
            AuthorId = authorId;
            Content = content;
            ParentCommentId = parentCommentId;
            CreatedAt = DateTime.UtcNow;
            IsHidden = false;
        }
    }
}
