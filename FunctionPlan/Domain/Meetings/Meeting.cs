using Domain.Common;
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
        public MeetingStatus Status { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public DateTime Created { get; private set; }

        private Meeting() { }

        public Meeting(string title, DateTime scheduledFor, int organizerId, double latitude, double longitude)
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
                throw new ArgumentException("Meeting organizer does not exist");
            }


            Title = title;
            ScheduledFor = scheduledFor;
            OrganizerId = organizerId;
            Latitude = latitude;
            Longitude = longitude;
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
        public void Cancell()
        {
            if (Status == MeetingStatus.Completed)
            {
                throw new InvalidMeetingStatus("Cannot cancell meeting that ended");
            }

            Status = MeetingStatus.Cancelled;
        }

    }
}
