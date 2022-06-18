using MediatR;

namespace Demo.Application
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
