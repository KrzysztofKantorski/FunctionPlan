namespace Application.Abstractions.Google
{
    public sealed record GoogleTokenInfo
    (
        string GoogleSubjectId,
        string Email,
        string Name
    );
}
