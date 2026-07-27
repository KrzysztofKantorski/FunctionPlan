using MediatR;

namespace Application.Abstractions.Messaging
{
    public interface ICommandBase
    {

    }
    public interface ICommand: IRequest
    {

    }

    public interface ICommand<out TResponse>: IRequest<TResponse>, ICommandBase
    {

    }
}
