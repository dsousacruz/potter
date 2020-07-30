using Potter.Domain.Handlers.Contracts;

namespace Potter.Domain.Commands.Responses
{
    public class GenericCommandResponse : IResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public GenericCommandResponse()
        {

        }

        public GenericCommandResponse(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
