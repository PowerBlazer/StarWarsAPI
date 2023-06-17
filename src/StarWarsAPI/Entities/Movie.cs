using System.ComponentModel.DataAnnotations;

namespace StarWarsAPI.Entities;

public class Movie: BaseEntity<int>
{
    /// <summary>
    /// Название
    /// </summary>
    [Required]
    public string? Title { get; set; }
    /// <summary>
    /// Персонаж
    /// </summary>
    public int CharacterId { get; set; }
    
    public Character? Character { get; set; }
}