namespace StarWarsAPI.Entities;

public class User: BaseEntity<int>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    
    public ICollection<Character>? Characters { get; set; }
}