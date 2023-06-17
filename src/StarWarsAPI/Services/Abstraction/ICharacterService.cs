using StarWarsAPI.Entities;

namespace StarWarsAPI.Services.Abstraction;

public interface ICharacterService
{
    /// <summary>
    /// Получить персонажей с пагинацией и поиском
    /// </summary>
    /// <param name="searchSetting">Value search</param>
    /// <param name="count">Character Count</param>
    /// <param name="startIndex">Start index characters</param>
    /// <returns>List characters</returns>
    Task<IList<Character>> GetCharactersAsync(string searchSetting,int count, int startIndex);
    /// <summary>
    /// Получить персонажа по айди
    /// </summary>
    /// <param name="id">Id Character</param>
    /// <returns>Character Object</returns>
    Task<Character?> GetCharacterByIdAsync(int id);
    /// <summary>
    /// Обновление данных о персонаже
    /// </summary>
    /// <param name="updatedCharacter"></param>
    /// <returns></returns>
    Task<Character> UpdateCharacterAsync(Character updatedCharacter);
    /// <summary>
    /// Создать нового персонажа
    /// </summary>
    /// <param name="newCharacter">New Character</param>
    /// <returns>New character</returns>
    Task<Character> CreateCharacterAsync(Character newCharacter);
    
    
}