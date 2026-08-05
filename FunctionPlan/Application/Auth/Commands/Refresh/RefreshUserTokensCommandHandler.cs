using Application.Abstractions.Security;
using Domain.Common;
using Domain.RefreshTokens;
using Domain.Users;
using Infrastructure.Security;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Auth.Commands.Refresh
{
    internal sealed class RefreshUserTokensCommandHandler: IRequestHandler<RefreshUserTokensCommand, TokenResponseDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenSettings _settings;
        private readonly IUserRepository _userRepository;

        public RefreshUserTokensCommandHandler(IRefreshTokenRepository refreshTokenRepository, IJwtProvider jwtService,
            IUnitOfWork unitOfWork, IRefreshTokenGenerator refreshTokenGenerator,
            IOptions<RefreshTokenSettings> settings, IUserRepository userRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _jwtProvider = jwtService;
            _unitOfWork = unitOfWork;
            _refreshTokenGenerator = refreshTokenGenerator;
            _settings = settings.Value;
            _userRepository = userRepository;
        }

        public async Task<TokenResponseDto> Handle(RefreshUserTokensCommand request, CancellationToken cancellationToken)
        {
            //check if refresh token is valid
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.refreshToken);

            if (refreshToken == null) 
            { 
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }

            //Check if refresh token is expired
            if (refreshToken.ExpiresAt < DateTime.UtcNow || refreshToken.IsRevoked)
            {
                throw new UnauthorizedAccessException("Refresh token is expired or revoked.");
            }

            //Revoke current refresh token
            refreshToken.Revoke();
            _refreshTokenRepository.Update(refreshToken);

            //Find user for JWT generation
            var userId = refreshToken.UserId;

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null) 
            { 
                throw new UnauthorizedAccessException("User not found.");
            }

            //Generate new refresh token
            var newRefreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            //Generate new access token
            var newAccessToken = _jwtProvider.GenerateToken(user);


            //Save new refresh token to the database
            var refreshTokenEntity = new RefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(_settings.ExpiryDays), userId);
           
            //Save changes
            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return new TokenResponseDto(newAccessToken, newRefreshToken);
        }
    }
}
