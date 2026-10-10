namespace Application.Users.Queries.GetUserStats
{
    internal sealed record UserStatsDto(
        int Id,
        string Username,
        string? ProfilePictureUrl,
        long OrganizedMeetingsCount,
        long AttendedMeetingsCount,
        long CommentsCount,
        long UploadedPhotosCount
        
    );
}
