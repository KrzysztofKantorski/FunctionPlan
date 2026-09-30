namespace Application.Users.Queries.GetUserStats
{
    internal sealed record UserStatsDto(
        long OrganizedMeetingsCount,
        long AttendedMeetingsCount,
        long CommentsCount,
        long UploadedPhotosCount
    );
}
