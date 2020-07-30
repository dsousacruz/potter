using Flunt.Notifications;
using Flunt.Validations;
using Potter.Domain.Handlers.Contracts;

namespace Potter.Domain.Commands.Requests
{
    public class CreateCharacterCommand : Notifiable, IRequest
    {
        public string Name { get; set; }

        public string Role { get; set; }

        public string School { get; set; }

        public string House { get; set; }

        public string Patronus { get; set; }

        public CreateCharacterCommand()
        {

        }

        public CreateCharacterCommand(string name, string role, string school, string house, string patronus)
        {
            Name = name;
            Role = role;
            School = school;
            House = house;
            Patronus = patronus;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .HasMinLen(Name, 1, "Nome", "Por favor, preencha o nome")
                .HasMaxLen(Name, 120, "Nome", "Máximo de 120 caracteres")
                .HasMinLen(Role, 1, "Regra", "Por favor, preencha a regra")
                .HasMaxLen(Role, 60, "Regra", "Máximo de 60 caracteres")
                .HasMinLen(School, 1, "Escola", "Por favor, preencha a escola")
                .HasMaxLen(School, 300, "Escola", "Máximo de 300 caracteres")
                .HasMinLen(House, 1, "Casa", "Por favor, preencha a casa")
            );
        }
    }
}
