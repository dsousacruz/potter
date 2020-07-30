using System.Threading.Tasks;

namespace Potter.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : IRequest
    {
        Task<IResponse> Handle(T command);
    }
}
