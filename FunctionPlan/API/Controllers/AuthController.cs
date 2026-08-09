using Application.Abstractions.Security.Tokens;
using Application.Auth.Commands.Email;
using Application.Auth.Commands.Login;
using Application.Auth.Commands.Logout;
using Application.Auth.Commands.Refresh;
using Application.Auth.Commands.RegisterUser;
using Application.Auth.Commands.VerifyUserEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace API.Controllers
{
    [Route("api/auth")]
    
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ISender _sender;
        private readonly RefreshTokenSettings _refreshTokenSettings;
        public AuthController(ISender sender, IOptions<RefreshTokenSettings> refreshTokenSettings)
        {
            _sender = sender;
            _refreshTokenSettings = refreshTokenSettings.Value;
        }

        [EnableRateLimiting("OtpRegisterLimit")]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
           [FromBody] RegisterUserCommand command,
           CancellationToken cancellationToken)
        {
            int userId = await _sender.Send(command, cancellationToken);
            return Created("", new { Id = userId });
        }

        [EnableRateLimiting("LoginLimit")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginUserCommand command,
            CancellationToken cancellationToken)
        {
            var tokenResponse = await _sender.Send(command, cancellationToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, 
                Secure = true, 
                SameSite = SameSiteMode.Strict, 
                Expires = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpiryDays) 
            };

            Response.Cookies.Append("refreshToken", tokenResponse.RefreshToken, cookieOptions);

            return Ok(new { AccessToken = tokenResponse.AccessToken });
        }


        [EnableRateLimiting("OtpRegisterLimit")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            //Get refresh token from cookie
            var refreshToken = Request.Cookies["refreshToken"];

            var command = new LogoutUserCommand(refreshToken);

            await _sender.Send(command, cancellationToken);

            // Remove the refresh token cookie
            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }

        [EnableRateLimiting("GlobalLimit")]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(CancellationToken cancellationToken)
        {
            //Get refresh token from cookie
            var oldRefreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(oldRefreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }

            var command = new RefreshUserTokensCommand(oldRefreshToken);

            var tokenResponse = await _sender.Send(command, cancellationToken);

            //Save new cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(_refreshTokenSettings.ExpiryDays)
            };

            Response.Cookies.Append("refreshToken", tokenResponse.refreshToken, cookieOptions);

            return Ok(new { AccessToken = tokenResponse.accessToken });
        }


        [EnableRateLimiting("OtpRegisterLimit")]
        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendTestEmail(
            [FromBody] SendTestEmailCommand command,
            CancellationToken cancellationToken)
        {
            await _sender.Send(command, cancellationToken);
            return Ok("Email sent successfully.");
        }


        [EnableRateLimiting("OtpRegisterLimit")]
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyUserOTP(
            [FromBody] VerifyUserEmailCommand command,
            CancellationToken cancellationToken
            )
        {
            await _sender.Send(command, cancellationToken);
            return Ok();
        }
    }
}
