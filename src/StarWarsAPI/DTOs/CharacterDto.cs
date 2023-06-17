namespace StarWarsAPI.DTOs;

public class CharacterDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public required string HomeWorld { get; set; }
    public string? Description { get; set; }
}