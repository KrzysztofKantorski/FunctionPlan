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
                throw new Exception("Provided date is invalid");
            }

            ScheduledFor = newDate;
        }


        //Cancell meeting
        public void Cancell()
        {
            if (Status == MeetingStatus.Completed)
            {
                throw new Exception("Cannot cancell meeting that ended");
            }

            Status = MeetingStatus.Cancelled;
        }

    }
}
