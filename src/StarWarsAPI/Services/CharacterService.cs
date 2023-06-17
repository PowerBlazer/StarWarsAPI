using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Context.Abstraction;
using StarWarsAPI.Entities;
using StarWarsAPI.Services.Abstraction;

namespace StarWarsAPI.Services;

public class CharacterService: ICharacterService
{
    private readonly IDatabaseContext _databaseContext;

    public CharacterService(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<IList<Character>> GetCharactersAsync(string searchSetting, int count, int startIndex)
    {
        var characters = await _databaseContext.Characters
            .Include(p => p.Movies)
            .Where(p => p.Name.Contains(searchSetting))
            .Take(count)
            .Skip(startIndex)
            .ToListAsync();

        return characters;
    }

    public async Task<Character?> GetCharacterByIdAsync(int id)
    {
        var character = await _databaseContext.Characters
            .Include(p=>p.Movies)
            .FirstOrDefaultAsync(p => p.Id == id);

        return character;
    }

    public async Task<Character> UpdateCharacterAsync(Character updatedCharacter)
    {
        var characterEntry = _databaseContext.Entry(updatedCharacter);
        
        if (characterEntry.State == EntityState.Detached)
        {
            _databaseContext.Characters.Attach(updatedCharacter);
        }
        
        _databaseContext.Entry(updatedCharacter).State = EntityState.Modified;

        await _databaseContext.SaveChangesAsync();

        return updatedCharacter;
    }

    public async Task<Character> CreateCharacterAsync(Character newCharacter)
    {
        var createdCharacter = await _databaseContext.Characters
            .AddAsync(newCharacter);
        
        await _databaseContext.SaveChangesAsync();

        return createdCharacter.Entity;
    }
}