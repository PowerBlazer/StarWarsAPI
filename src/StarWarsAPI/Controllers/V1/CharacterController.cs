using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.DTOs;
using StarWarsAPI.Entities;
using StarWarsAPI.Services.Abstraction;

namespace StarWarsAPI.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]"),ApiVersion("1.0")]
public class CharacterController: BaseController
{
    private readonly ICharacterService _characterService;
    private readonly IMapper _mapper;

    public CharacterController(ICharacterService characterService, 
        IMapper mapper)
    {
        _characterService = characterService;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Получение персонажей с пагинацией и поиском по имени
    /// </summary>
    /// <param name="searchValue">Значение поиска (name)</param>
    /// <param name="page">Страница</param>
    /// <param name="count">Количество записей</param>
    /// <returns></returns>
    /// <response code="200">Список персонажей</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpGet("characters")]
    [ProducesResponseType(typeof(IList<CharacterDto>),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCharacters(string searchValue = "",int page = 1,int count = 10)
    {
        var characters = await _characterService.GetCharactersAsync(
            searchValue,
            count,
            (page-1)*10);

        var charactersDto = characters
            .Select(p => _mapper.Map<Character, CharacterDto>(p))
            .ToList();

        return Ok(charactersDto);
    }
    
    /// <summary>
    /// Получить подробную информацию персонажа
    /// </summary>
    /// <param name="id">Id персонажа</param>
    /// <returns></returns>
    /// <response code="200">Возвращает список сообщении группового чата</response>
    /// <response code="404">Персонаж не найден</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCharacterById(int id)
    {
        var character = await _characterService.GetCharacterByIdAsync(id);

        if (character is null)
        {
            return NotFound("Персонаж не найден");
        }

        var characterDto = _mapper.Map<Character, CharacterCardDto>(character);

        return Ok(characterDto);
    }
    
    /// <summary>
    /// Редактировать данные о персонаже
    /// </summary>
    /// <param name="id">Id персонажа</param>
    /// <param name="updateCharacterDto">Новая информация о персонаже</param>
    /// <returns></returns>
    ///<response code="200">Возвращает обновленного персонажа</response>
    ///<response code="400">Ошибка доступа к персонажу</response>
    ///<response code="404">Персонаж не найден</response>
    ///<response code="500">Ошибка на сервере</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(UpdateCharacterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCharacter(int id, 
        [FromBody] UpdateCharacterDto updateCharacterDto)
    {
        var character = await _characterService.GetCharacterByIdAsync(id);

        if (character is null) 
            return NotFound("Персонаж не найден");

        if (character.OwnerId is not null && character.OwnerId != UserId)
            return BadRequest("Доступ к персонажу запрещен");
        
        _mapper.Map(updateCharacterDto, character);

        var updatedCharacter = _mapper
            .Map<Character,UpdateCharacterDto>(await _characterService.UpdateCharacterAsync(character));

        return Ok(updatedCharacter);

    }

    /// <summary>
    /// Создание персонажа
    /// </summary>
    /// <param name="createCharacterDto">Информация о персонаже</param>
    /// <returns></returns>
    /// <response code="200">Возвращает созданного персонажа</response>
    /// <response code="500">Ошибка на сервере</response>
    [HttpPost]
    [ProducesResponseType(typeof(UpdateCharacterDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterDto createCharacterDto)
    {
        var createCharacter = _mapper.Map<CreateCharacterDto, Character>(createCharacterDto);

        if (UserId is not null)
        {
            createCharacter.OwnerId = UserId;
        }
        
        var createdCharacter = await _characterService.CreateCharacterAsync(createCharacter);
        var createdCharacterDto = _mapper.Map<Character, CharacterCardDto>(createdCharacter);
        
        return Ok(createdCharacterDto);
    }
}