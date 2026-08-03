using Application.Abstractions.Security;
using Domain.Common;
using Domain.RefreshTokens;
using Domain.Users;
using MediatR;

namespace Application.Auth.Commands.Login
{
    internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenResponseDto>
    {
        //we need repository
        //We need unit of worl
        //We need password hasher
        //We need token generator

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public LoginUserCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository,
            IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IRefreshTokenGenerator refreshTokenGenerator) 
        { 
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<TokenResponseDto?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // Check if user with provided email exists
            var existingUser = await _userRepository.GetByEmailAddressAsync(request.Email);

            if (existingUser == null) 
            { 
                throw new ArgumentException("User not found");
            }

            //Check if user is registered with Google account
            if (existingUser.GoogleSubjectId != null)
            {
                throw new ArgumentException("User is registered with Google. Please use Google login.");
            }
          
            string existingPassword = existingUser.PasswordHash!;

            //Check password
            var isPasswordValid = _passwordHasher.Verify(request.Password, existingUser.PasswordHash!);

            if(!isPasswordValid)
            {
                throw new ArgumentException("Invalid password");
            }

            //generate access token
            var accessToken = _jwtProvider.GenerateToken(existingUser);

            var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();


            //Save refresh token
            var refreshTokenEntity = new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(7), existingUser.Id);

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new TokenResponseDto(
                accessToken,
                refreshToken
            );


        }
    }
}
