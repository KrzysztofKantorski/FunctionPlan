using Application.Abstractions.Messaging;

namespace Application.Auth.Commands.VerifyUserEmail
{
    public sealed record VerifyUserEmailCommand(
        string Email,
        string OTP
    ): ICommand;
}
