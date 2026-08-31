namespace Application.Abstractions.Google
{
    public interface IGoogleAuthService
    {
        Task<GoogleTokenInfo> VerifyGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default);
    }
}
