using Application.Abstractions.Cache;
using Application.Exceptions;
using Domain.Common;
using Domain.Users;
using MediatR;

namespace Application.Auth.Commands.VerifyUserEmail
{
    internal sealed class VerifyUserEmailCommandHandler : IRequestHandler<VerifyUserEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        public VerifyUserEmailCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ICacheService cacheService) 
        { 
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
        {
            //Check if user exists
            var user = await _userRepository.GetByEmailAddressAsync(request.Email);

            if (user is null)
            {
                throw new UserNotFoundException("User was not found");
            }

            //Check if user is alerdy verified
            if (user.IsVerified)
            {
                throw new UserAlerdyVerifiedException("User alerdy verified");
            }


            string redisKey = $"otp:verification:user:{user.Id}";

            //Get data from redis
            var savedOtp = await _cacheService.GetAsync<string>(redisKey, cancellationToken);

            if(savedOtp is null || request.OTP != savedOtp)
            {
                throw new InvalidUserDataException("Incorrect or expired verification code");
            }


            //User sent proper OTP - clear redis 
            await _cacheService.RemoveAsync(redisKey, cancellationToken);

            //Update isVerified flag
            user.verify();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
