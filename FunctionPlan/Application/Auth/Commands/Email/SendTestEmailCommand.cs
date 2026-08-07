using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.Email
{
    public sealed record SendTestEmailCommand(
        string To,
        string Subject,
        string Body
    ) : ICommand;
}
