namespace Application.Media.Queries.GetMeetingMedia
{
    public sealed record MeetingMediaDto(
        string FileName,
        string? Description,
        DateTime CreatedAt,
        string UploaderName,
        string? UploaderAvatarId
    );
}
