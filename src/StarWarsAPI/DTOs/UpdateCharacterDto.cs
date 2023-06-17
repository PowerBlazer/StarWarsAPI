namespace StarWarsAPI.DTOs;

public class UpdateCharacterDto
{
    public required string Name { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public required string HomeWorld { get; set; }
    public string? Gender { get; set; }
    public string? Species { get; set; }
    public int Height { get; set; }
    public string? HairColor { get; set; }
    public string? EyeColor { get; set; }
    public string? Description { get; set; }
}