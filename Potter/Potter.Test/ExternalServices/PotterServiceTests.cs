using Microsoft.VisualStudio.TestTools.UnitTesting;
using Potter.Domain.Services;
using Potter.Infra.CrossCutting.ExternalServices.PotterApi;
using System.Threading.Tasks;

namespace Potter.Tests.HandlerTests
{
    [TestClass]
    public class PotterServiceTests
    {
        private readonly string _potterUrl = "https://www.potterapi.com/v1/";
        private readonly string _potterKey = "$2a$10$Gv2j7QtT7dO0CuIEaVOfcuVemz2W7.daT96/u/VojjXoNnWSYERGS";

        private readonly string _validHouseId = "5a05e2b252f721a3cf2ea33f";
        private readonly string _invalidHouseId = "INVALID_ID";

        private readonly IPotterService _service;

        public PotterServiceTests()
        {
            _service = new PotterService(_potterUrl, _potterKey);
        }

        [TestMethod]
        public async Task Dado_uma_house_invalida_deve_interromper_execucao()
        {
            var result = await _service.ValidateHouse(_invalidHouseId);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public async Task Dado_uma_house_valida_deve_continuar_execucao()
        {
            var result = await _service.ValidateHouse(_validHouseId);

            Assert.AreEqual(true, result);
        }
    }
}
