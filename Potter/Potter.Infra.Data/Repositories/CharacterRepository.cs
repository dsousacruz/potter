using Microsoft.EntityFrameworkCore;
using Potter.Domain.Entities;
using Potter.Domain.Repositories;
using Potter.Infra.Data.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Potter.Infra.Data.Repositories
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly DataContext _context;

        public CharacterRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Create(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);

            if (character != null)
                _context.Remove(character);
        }

        public async Task<IEnumerable<Character>> Get(string house)
        {
            var result = (IEnumerable<Character>)_context.Characters
                .AsNoTracking()
                .Where(x => string.IsNullOrEmpty(house) ? "1" == "1" : x.House == house)
                .OrderBy(x => x.Name);

            return await Task.FromResult(result);
        }

        public async Task<Character> GetById(string id)
        {
            return await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
