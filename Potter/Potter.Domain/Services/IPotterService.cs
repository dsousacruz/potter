using System.Threading.Tasks;

namespace Potter.Domain.Services
{
    public interface IPotterService
    {
        Task<bool> ValidateHouse(string house);
    }
}
