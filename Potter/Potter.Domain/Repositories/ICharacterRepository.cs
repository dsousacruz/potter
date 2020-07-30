using Potter.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Potter.Domain.Repositories
{
    public interface ICharacterRepository
    {
        Task Create(Character character);

        Task Update(Character character);

        Task Delete(string id);

        Task<Character> GetById(string id);

        Task<IEnumerable<Character>> Get(string house);
    }
}
