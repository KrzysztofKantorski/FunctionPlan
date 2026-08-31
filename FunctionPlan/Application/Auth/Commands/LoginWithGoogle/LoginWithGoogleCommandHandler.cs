using Application.Abstractions.Google;
using Application.Abstractions.Security.Tokens;
using Application.Common.Dto;
using Domain.Common;
using Domain.RefreshTokens;
using Domain.Users;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Auth.Commands.LoginWithGoogle
{
    internal sealed class LoginWithGoogleCommandHandler : IRequestHandler<LoginWithGoogleCommand, TokenResponseDto>
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly RefreshTokenSettings _settings;
        public LoginWithGoogleCommandHandler(IGoogleAuthService googleAuthService, IUserRepository userRepository,
            IUnitOfWork unitOfWork, IRefreshTokenRepository refreshTokenRepository, IJwtProvider jwtProvider,
            IRefreshTokenGenerator refreshTokenGenerator, IOptions<RefreshTokenSettings> settings) 
        {
            _googleAuthService = googleAuthService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtProvider = jwtProvider;
            _refreshTokenGenerator = refreshTokenGenerator;
            _settings = settings.Value;
        }

        public async Task<TokenResponseDto> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {

            //Get user data
            var userData = await _googleAuthService.VerifyGoogleTokenAsync(request.GoogleIdToken, cancellationToken);

            if(userData is null)
            {
                throw new Exception("Incorrect user token");
            }

            //Check if user alerdy signed with google
            var user = await _userRepository.GetByGoogleSubjectIdAsync(userData.GoogleSubjectId);

            if (user is null)
            {
                user = await _userRepository.GetByEmailAddressAsync(userData.Email, cancellationToken);

                if (user is not null)
                {
                    //Save users google subject id
                    user.AddGoogleIdentity(userData.GoogleSubjectId);
                }
                else
                {
                    //Register new user
                    user = User.CreateWithGoogle(
                        userData.Name,
                        userData.Email,
                        userData.GoogleSubjectId
                    );

                    //Google alerdy verified user
                    user.verify();

                    //Add new user
                    await _userRepository.AddAsync(user);
                }

                await _unitOfWork.SaveChangesAsync();

            }


            //Generate auth tokens
            string accessToken = _jwtProvider.GenerateToken(user);
            string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            //Save refresh token
            var refreshTokenEntity = new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(_settings.ExpiryDays), user.Id);

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponseDto(accessToken, refreshToken);
        }
    }
}
