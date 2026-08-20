using Domain.Comments;
using Domain.Common;
using Domain.Users;
namespace Domain.Meetings
{
    public enum MeetingStatus
    {
        Planned,
        InProgress,
        Completed,
        Cancelled
    }

    public sealed class Meeting : Entity
    {
        public string Title { get; private set; }
        public DateTime ScheduledFor { get; private set; }
        public int OrganizerId { get; private set; }
        public User Organizer { get; private set; }
        public MeetingStatus Status { get; private set; }
        public Coordinates Location {  get; private set; }
        public DateTime Created { get; private set; }

        private readonly List<User> _users = new();

        public IReadOnlyCollection<User> Users => _users.AsReadOnly();

        private readonly List<Comment> _comments = new();
        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        private Meeting() { }

        public Meeting(string title, DateTime scheduledFor, int organizerId, Coordinates location)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("Title cannot be empty");
            }

            if(title.Length > 100)
            {
                throw new ArgumentException("Incorrect title content");
            }

            if(scheduledFor < DateTime.UtcNow)
            {
                throw new InvalidMeetingDateException("Incorrect meeting date: " + scheduledFor);
            }

            if (organizerId < 0) 
            {
                throw new InvalidUserException("Meeting organizer does not exist");
            }


            Title = title;
            ScheduledFor = scheduledFor;
            OrganizerId = organizerId;
            Location = location;
            Status = MeetingStatus.Planned;
            Created = DateTime.UtcNow;
        }

        //Reschedule meeting
        public void Reschedule(DateTime newDate)
        {
            if (newDate < DateTime.UtcNow)
            {
                throw new InvalidMeetingDateException("Provided date is invalid");
            }

            ScheduledFor = newDate;
        }


        //Cancell meeting
        public void Cancel()
        {
            if (Status == MeetingStatus.Completed)
            {
                throw new InvalidMeetingStatus("Cannot cancell meeting that ended");
            }

            if (Status == MeetingStatus.Cancelled)
            {
                throw new InvalidMeetingStatus("Meeting is already cancelled.");
            }

            Status = MeetingStatus.Cancelled;
        }



        //Mark meeting as completed
        public void MarkAsCompleted()
        {
            if (Status == MeetingStatus.Cancelled)
            {
                throw new InvalidMeetingStatus("Cannot complete cancelled meeting");
            }

            if (Status == MeetingStatus.Completed)
            {
                throw new InvalidMeetingStatus("Cannot complete alerdy completed meeting");
            }

            Status = MeetingStatus.Completed;
        }



        //Change coordinates
        public void ChangeLocation(Coordinates newLocation)
        {
            Location = newLocation ?? throw new IncorrectLocationException(nameof(newLocation));
        }


        //Confirm attendence
        public void ConfirmAttendence(User user)
        {
            if(user is null)
            {
                throw new InvalidUserException("Incorrect user");
            }

            if (Status == MeetingStatus.Completed || Status == MeetingStatus.Cancelled)
            {
                throw new InvalidMeetingStatus("Cannot change attendance for a completed or cancelled meeting");
            }

            //Chcek if user alerdy confirmed attendence
            if (_users.Any(u => u.Id == user.Id))
            {
                throw new InvalidUserException("User alerdy assigned to meeting");
            }

            //Check if user is organizer
            if(user.Id == OrganizerId)
            {
                throw new InvalidUserException("Organizer is alerdy in meeting");
            }

            _users.Add(user);
        }


        //Cancel attendence
        public void CancelAttendence(User user)
        {
            if (user is null)
            {
                throw new InvalidUserException("Incorrect user");
            }

            if (Status == MeetingStatus.Completed || Status == MeetingStatus.Cancelled)
            {
                throw new InvalidMeetingStatus("Cannot change attendance for a completed or cancelled meeting");
            }

            //Check if user is organizer
            if (user.Id == OrganizerId)
            {
                throw new InvalidUserException("Organizer cannot leave meeting");
            }

            //Chcek if user alerdy confirmed attendence
            if (!_users.Any(u => u.Id == user.Id))
            {
                throw new InvalidUserException("User is not assigned to meeting");
            }


            var userToRemove = _users.FirstOrDefault(u => u.Id == user.Id);

            if (userToRemove is null)
            {
                throw new InvalidUserException("User is not assigned to this meeting");
            }

            _users.Remove(userToRemove);
        }


        //Add comment
        public void AddComment(User user, string content, int? parentCommentId = null)
        {
            if(user is null)
            {
                throw new InvalidUserException("Incorrect user");
            }

            var comment = new Comment(this.Id, user.Id, content, parentCommentId);

            _comments.Add(comment);
        }
    }
}
