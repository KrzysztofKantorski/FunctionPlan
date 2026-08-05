using Domain.Common;
using Domain.RefreshTokens;
using MediatR;

namespace Application.Auth.Commands.Logout
{
    internal class LogoutUserCommandHandler: IRequestHandler<LogoutUserCommand>
    {

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LogoutUserCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            //Check if  refresh token is valid
            if (request.refreshToken == null)
            {
                throw new Exception("Incorrect token");
            }

            //Check if refresh token exists in db
            var refreshToken = await _refreshTokenRepository.GetByTokenAsync(request.refreshToken);

            if (refreshToken == null || refreshToken.IsRevoked)
            {
                return;
            }

            refreshToken.Revoke();

            _refreshTokenRepository.Update(refreshToken);

            //Save changes to db
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
