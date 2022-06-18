using MediatR;

namespace Demo.Application
{
    public interface ICommand : IRequest
    {
    }

    public interface ICommand<out TResult> : IRequest<TResult>
    {
    }
}
