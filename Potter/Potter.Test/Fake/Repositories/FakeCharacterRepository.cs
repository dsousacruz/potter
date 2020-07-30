using Potter.Domain.Entities;
using Potter.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Potter.Tests.Fake.Repositories
{

    public class FakeCharacterRepository : ICharacterRepository
    {
        public Task Create(Character character)
        {
            return Task.FromResult(0);
        }

        public Task Delete(string id)
        {
            return Task.FromResult(0);
        }

        public Task<IEnumerable<Character>> Get(string house)
        {
            return Task.FromResult((IEnumerable<Character>)new List<Character>());
        }

        public Task<Character> GetById(string id)
        {
            if (id.Equals("INVALID_ID"))
                return null;

            return Task.FromResult(new Character("Harry", "stutend", "", "", ""));
        }

        public Task Update(Character character)
        {
            return Task.FromResult(0);
        }
    }

}