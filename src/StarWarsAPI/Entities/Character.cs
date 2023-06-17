namespace StarWarsAPI.Entities;

public class Character: BaseEntity<int>
{
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// День рождение
    /// </summary>
    public DateTimeOffset BirthDate { get; set; }
    /// <summary>
    /// Родная планета
    /// </summary>
    public required string HomeWorld { get; set; }
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    /// <summary>
    /// Раса
    /// </summary>
    public string? Species { get; set; }
    /// <summary>
    /// Рост
    /// </summary>
    public int Height { get; set; }
    /// <summary>
    /// Цвет волос
    /// </summary>
    public string? HairColor { get; set; }
    /// <summary>
    /// Цвет глаз
    /// </summary>
    public string? EyeColor { get; set; }
    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }
    /// <summary>
    /// Создатель персонажа
    /// </summary>
    public int? OwnerId { get; set; }
    
    public User? Owner { get; set; }
    public ICollection<Movie>? Movies { get; set; }
}