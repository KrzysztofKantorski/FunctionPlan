namespace Application.Media.Queries.GetUsersAvatars
{
    public sealed record MeetingAuthDto(
       int Status,
       int OrganizerId,
       bool IsParticipant
   );
}
