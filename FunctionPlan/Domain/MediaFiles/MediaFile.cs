using Domain.Common;
using Domain.Meetings;
using Domain.Users;

namespace Domain.Media
{
    public sealed class MediaFile: Entity
    {
        public int MeetingId { get; private set; }
        public Meeting Meeting { get; private set; }
        public int UploaderId { get; private set; }
        public User Uploader { get; private set; }
        public string FileName { get; private set; }
        public string? Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        private MediaFile() { }

        public MediaFile(int meetingId, int uploaderId, string fileName, string? description)
        {
            if(meetingId < 0)
            {
                throw new IncorrectMeetingException("Incorrect meeting id");
            }

            if (uploaderId < 0)
            {
                throw new IncorrectUserException("Incorrect uploader id");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new IncorrectFileName("Incorrect image url");
            }

            if (description != null && description.Length > 200)
            {
                throw new IncorrectImageDescription("Incorrect image description");
            }

            MeetingId = meetingId;
            UploaderId = uploaderId;
            FileName = fileName;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }


        //Update media description
        public void updateMediaDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription) || newDescription.Length > 200)
            {
                throw new IncorrectImageDescription("Incorrect image description");
            }

            Description = newDescription;
        }
    }
}
