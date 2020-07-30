using Potter.Domain.Services;
using System.Threading.Tasks;

namespace Potter.Tests.Fake.Services
{
    public class FakePotterService : IPotterService
    {
        public async Task<bool> ValidateHouse(string house)
        {
            return await Task.FromResult(house.Equals("5a05e2b252f721a3cf2ea33f"));
        }
    }
}
