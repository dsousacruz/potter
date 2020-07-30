using Flunt.Notifications;
using Potter.Domain.Commands.Requests;
using Potter.Domain.Commands.Responses;
using Potter.Domain.Entities;
using Potter.Domain.Handlers.Contracts;
using Potter.Domain.Repositories;
using Potter.Domain.Services;
using System.Threading.Tasks;

namespace Potter.Domain.Handlers
{
    public class CharacterHandler :
        IHandler<CreateCharacterCommand>,
        IHandler<UpdateCharacterCommand>,
        IHandler<DeleteCharacterCommand>
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IPotterService _potterService;

        public CharacterHandler(ICharacterRepository characterRepository, IPotterService potterService)
        {
            _characterRepository = characterRepository;
            _potterService = potterService;
        }

        public async Task<IResponse> Handle(CreateCharacterCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResponse(false, "Comando Inválido", command.Notifications);

            var houseIsValid = await _potterService.ValidateHouse(command.House);

            if (!houseIsValid)
                return new GenericCommandResponse(false, "Comando Inválido", new Notification("House", "Casa não encontrada"));

            var character = new Character(command.Name, command.Role, command.School, command.House, command.Patronus);
            await _characterRepository.Create(character);

            return new GenericCommandResponse(true, "OK", character);
        }

        public async Task<IResponse> Handle(UpdateCharacterCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResponse(false, "Comando Inválido", command.Notifications);

            var houseIsValid = await _potterService.ValidateHouse(command.House);

            if (!houseIsValid)
                return new GenericCommandResponse(false, "Comando Inválido", new Notification("House", "Casa não encontrada"));

            var character = await _characterRepository.GetById(command.Id);

            if (character == null)
                return new GenericCommandResponse(false, "Comando Inválido", new Notification("Id", "Personagem não encontrada"));

            character.Update(command.Name, command.Role, command.School, command.House, command.Patronus);

            await _characterRepository.Update(character);

            return new GenericCommandResponse(true, "OK", character);
        }

        public async Task<IResponse> Handle(DeleteCharacterCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResponse(false, "Comando Inválido", command.Notifications);

            await _characterRepository.Delete(command.Id);

            return new GenericCommandResponse(true, "OK", command);
        }
    }
}
