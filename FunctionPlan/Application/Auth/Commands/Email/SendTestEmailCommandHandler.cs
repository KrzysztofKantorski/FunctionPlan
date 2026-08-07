using Application.Abstractions.Mail;
using MediatR;


namespace Application.Auth.Commands.Email
{
    internal sealed class SendTestEmailCommandHandler : IRequestHandler<SendTestEmailCommand>
    {
        private readonly IEmailSender _sender;
        public SendTestEmailCommandHandler(IEmailSender sender) 
        { 
            _sender = sender;
        }
        public async Task Handle(SendTestEmailCommand request, CancellationToken cancellationToken)
        {
            //Just send email
            await _sender.SendEmailAsync(request.To, request.Subject, request.Body, cancellationToken);
        }
    }
}
