using Flunt.Notifications;
using Flunt.Validations;
using Potter.Domain.Handlers.Contracts;

namespace Potter.Domain.Commands.Requests
{
    public class DeleteCharacterCommand : Notifiable, IRequest
    {
        public string Id { get; set; }

        public DeleteCharacterCommand()
        {

        }

        public DeleteCharacterCommand(string id)
        {
            Id = id;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Id, 1, "Id", "Por favor, informe o Id")
            );
        }
    }
}
