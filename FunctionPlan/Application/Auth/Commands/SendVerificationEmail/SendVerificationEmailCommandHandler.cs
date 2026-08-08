using Application.Abstractions.Cache;
using Application.Abstractions.Mail;
using Application.Abstractions.Security;
using Domain.Users;
using MediatR;
namespace Application.Auth.Commands.UserVerification
{
    internal sealed class SendVerificationEmailCommandHandler : IRequestHandler<SendVerificationEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPGenerator _otpGenerator;
        private readonly ICacheService _cacheService;
        private readonly IEmailSender _emailService;

        public SendVerificationEmailCommandHandler(IUserRepository userRepository, IOTPGenerator otpGenerator,
            ICacheService cacheService, IEmailSender emailService)
        {
            _userRepository = userRepository;
            _otpGenerator = otpGenerator;
            _cacheService = cacheService;
            _emailService = emailService;
        }

        public async Task Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {

            //Check if user is registered
            var existingUser = await _userRepository.GetByEmailAddressAsync(request.email, cancellationToken);

            if(existingUser == null)
            {
                throw new Exception("This user does not exist");
            }

            //Check if user is verified
            if(existingUser.IsVerified)
            {
                throw new Exception("This user is already verified");
            }

            //Generate OTP
            var verificationCode = _otpGenerator.GenerateOTP();

            //Redis key
            string redisKey = $"otp:verification:user:{existingUser.Id}";

            //Save code and id to redis
            await _cacheService.SetAsync(redisKey, verificationCode, TimeSpan.FromMinutes(10), cancellationToken);

            //Send email with code to user
            string subject = "Verification Code";
            string body = $"Your verification code is: {verificationCode}";

            await _emailService.SendEmailAsync(existingUser.Email, subject, body, cancellationToken);
        }
    }
}
