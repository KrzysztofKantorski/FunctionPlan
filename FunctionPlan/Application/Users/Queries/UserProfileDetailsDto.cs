namespace Application.Users.Queries
{
    public sealed class UserProfileDetailsDto
    {
        public int Id { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string ProfilePictureUrl { get; init; }
    }
}
