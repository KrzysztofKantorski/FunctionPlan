using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.UserVerification
{
    public sealed record SendVerificationEmailCommand(
        string Email):ICommand;
}
