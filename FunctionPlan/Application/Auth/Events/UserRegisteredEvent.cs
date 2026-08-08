using MediatR;

namespace Application.Auth.Events
{
    public sealed record UserRegisteredEvent
    (string Email): INotification;
}
