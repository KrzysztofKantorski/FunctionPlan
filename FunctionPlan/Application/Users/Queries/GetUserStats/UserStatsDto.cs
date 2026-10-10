namespace Application.Users.Queries.GetUserStats
{
    internal sealed record UserStatsDto(
        int Id,
        string Username,
        long OrganizedMeetingsCount,
        long AttendedMeetingsCount,
        long CommentsCount,
        long UploadedPhotosCount

    );
}
