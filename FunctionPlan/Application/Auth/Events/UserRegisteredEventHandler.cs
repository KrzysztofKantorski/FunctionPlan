using Application.Auth.Commands.UserVerification;
using MediatR;

namespace Application.Auth.Events
{
    internal class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        private readonly ISender _sender;
        public UserRegisteredEventHandler(ISender sender)
        {
            _sender = sender;
        }
        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var command = new SendVerificationEmailCommand(notification.Email);
            await _sender.Send(command, cancellationToken);
        }
    }
}
