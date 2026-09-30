namespace Application.Users.Queries.GetUserStats
{
    internal sealed record UserStatsDto(
        int OrganizedMeetingsCount,
        int AttendedMeetingsCount,
        int CommentsCount,
        int UploadedPhotosCount
    );
}
