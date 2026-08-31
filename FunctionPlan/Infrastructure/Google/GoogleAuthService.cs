using Application.Abstractions.Google;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;


namespace Infrastructure.Google
{
    internal sealed class GoogleAuthService : IGoogleAuthService
    {

        private readonly GoogleAuthSettings _settings;

        public GoogleAuthService(IOptions<GoogleAuthSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<GoogleTokenInfo> VerifyGoogleTokenAsync(string idToken, CancellationToken cancellationToken = default)
        {

            try
            {
                //Validate client id
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _settings.ClientId }

                };

                //Verify id token
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

                var authResponse = new GoogleTokenInfo(
                    payload.Subject,
                    payload.Email,
                    payload.Name
                );

                return authResponse;

            }
            catch(InvalidJwtException)
            {
                throw new UnauthorizedAccessException("Invalid or manipulated Google token.");
            }

        }
    }
}
