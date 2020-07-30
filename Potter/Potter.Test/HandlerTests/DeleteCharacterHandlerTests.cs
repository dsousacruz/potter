using Microsoft.VisualStudio.TestTools.UnitTesting;
using Potter.Domain.Commands.Requests;
using Potter.Domain.Handlers;
using Potter.Tests.Fake.Repositories;
using Potter.Tests.Fake.Services;
using System.Threading.Tasks;

namespace Potter.Tests.HandlerTests
{
    [TestClass]
    public class DeleteCharacterHandlerTests
    {
        private readonly DeleteCharacterCommand _invalidCommand = new DeleteCharacterCommand();
        private readonly DeleteCharacterCommand _validCommand = new DeleteCharacterCommand("VALID_ID");

        private readonly CharacterHandler _handler = new CharacterHandler(new FakeCharacterRepository(), new FakePotterService());

        [TestMethod]
        public async Task Dado_um_comando_invalido_deve_interromper_execucao()
        {
            var result = await _handler.Handle(_invalidCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public async Task Dado_um_comando_valido_deve_excluir_a_personagem()
        {
            var result = await _handler.Handle(_validCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
