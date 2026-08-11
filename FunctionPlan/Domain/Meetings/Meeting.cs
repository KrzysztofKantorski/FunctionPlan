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

            //Chcek if user alerdy confirmed attendence
            if(_users.Any(u => u.Id == user.Id))
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
    }
}
