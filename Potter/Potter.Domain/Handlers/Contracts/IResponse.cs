
namespace Potter.Domain.Handlers.Contracts
{
    public interface IResponse
    {
        bool Success { get; set; }

        string Message { get; set; }

        object Data { get; set; }
    }
}