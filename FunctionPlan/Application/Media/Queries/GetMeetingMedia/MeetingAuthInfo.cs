namespace Application.Media.Queries.GetMeetingMedia
{
    public sealed record MeetingAuthInfo(
        int Status, 
        int OrganizerId, 
        bool IsParticipant
    );
}
