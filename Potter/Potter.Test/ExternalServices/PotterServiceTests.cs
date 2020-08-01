using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Potter.Domain.Services;
using Potter.Infra.CrossCutting.ExternalServices.PotterApi;
using System.Threading.Tasks;

namespace Potter.Tests.HandlerTests
{
    [TestClass]
    public class PotterServiceTests
    {
        private readonly string _validHouseId = "5a05e2b252f721a3cf2ea33f";
        private readonly string _invalidHouseId = "INVALID_ID";

        private readonly IPotterService _service;

        public PotterServiceTests()
        {
            var _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "PotterUrl")]).Returns("https://www.potterapi.com/v1/");
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "PotterKey")]).Returns("$2a$10$Gv2j7QtT7dO0CuIEaVOfcuVemz2W7.daT96/u/VojjXoNnWSYERGS");

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "AppSettings"))).Returns(_mockConfSection.Object);

            var mockLogger = new Mock<ILogger<PotterService>>();

            _service = new PotterService(mockConfiguration.Object, mockLogger.Object);
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
