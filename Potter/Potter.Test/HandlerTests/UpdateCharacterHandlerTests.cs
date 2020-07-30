using Microsoft.VisualStudio.TestTools.UnitTesting;
using Potter.Domain.Commands.Requests;
using Potter.Domain.Handlers;
using Potter.Tests.Fake.Repositories;
using Potter.Tests.Fake.Services;
using System.Threading.Tasks;

namespace Potter.Tests.HandlerTests
{
    [TestClass]
    public class UpdateCharacterHandlerTests
    {
        private readonly UpdateCharacterCommand _invalidCommand = new UpdateCharacterCommand();
        private readonly UpdateCharacterCommand _validCommand = new UpdateCharacterCommand("VALID_ID", "Harry Potter Updated", "student", "Hogwarts School of Witchcraft and Wizardry", "5a05e2b252f721a3cf2ea33f", "stag");
        private readonly UpdateCharacterCommand _invalidHouseCommand = new UpdateCharacterCommand("VALID_ID", "Harry Potter", "student", "Hogwarts School of Witchcraft and Wizardry", "AAA", "stag");
        private readonly UpdateCharacterCommand _invalidCharacterIdCommand = new UpdateCharacterCommand("INVALID_ID", "Harry Potter", "student", "Hogwarts School of Witchcraft and Wizardry", "AAA", "stag");

        private readonly CharacterHandler _handler = new CharacterHandler(new FakeCharacterRepository(), new FakePotterService());

        [TestMethod]
        public async Task Dado_um_comando_invalido_deve_interromper_execucao()
        {
            var result = await _handler.Handle(_invalidCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public async Task Dado_uma_casa_invalida_deve_interromper_execucao()
        {
            var result = await _handler.Handle(_invalidHouseCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public async Task Dado_uma_personagem_invalida_deve_interromper_execucao()
        {
            var result = await _handler.Handle(_invalidCharacterIdCommand);

            Assert.AreEqual(false, result.Success);
        }

        [TestMethod]
        public async Task Dado_um_comando_valido_deve_atualizar_a_personagem()
        {
            var result = await _handler.Handle(_validCommand);

            Assert.AreEqual(true, result.Success);
        }
    }
}
