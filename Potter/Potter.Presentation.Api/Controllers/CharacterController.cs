using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Potter.Domain.Commands.Requests;
using Potter.Domain.Commands.Responses;
using Potter.Domain.Entities;
using Potter.Domain.Handlers;
using Potter.Domain.Repositories;

namespace Potter.Presentation.Api.Controllers
{
    [ApiController]
    [Route("v1/characters")]
    public class CharacterController : ControllerBase
    {
        public CharacterController()
        {
        }

        [Route("")]
        [HttpPost]
        public async Task<GenericCommandResponse> Create([FromBody] CreateCharacterCommand command, [FromServices] CharacterHandler handler)
        {
            return (GenericCommandResponse)await handler.Handle(command);
        }

        [Route("")]
        [HttpPut]
        public async Task<GenericCommandResponse> Update([FromBody] UpdateCharacterCommand command, [FromServices] CharacterHandler handler)
        {
            return (GenericCommandResponse)await handler.Handle(command);
        }

        [Route("")]
        [HttpDelete]
        public async Task<GenericCommandResponse> Delete([FromBody] DeleteCharacterCommand command, [FromServices] CharacterHandler handler)
        {
            return (GenericCommandResponse)await handler.Handle(command);
        }

        [Route("")]
        [HttpGet]
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 5)]
        public async Task<IEnumerable<Character>> Get([FromQuery] string house, [FromServices] ICharacterRepository characterRepository)
        {
            return await characterRepository.Get(house);
        }
    }
}
