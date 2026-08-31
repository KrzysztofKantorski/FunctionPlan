using Application.Abstractions.Messaging;
using Application.Common.Dto;

namespace Application.Auth.Commands.LoginWithGoogle
{
    public sealed record LoginWithGoogleCommand(
        string GoogleIdToken
    ):ICommand<TokenResponseDto>;
}
