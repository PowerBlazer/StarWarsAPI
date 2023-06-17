using System.ComponentModel.DataAnnotations;

namespace StarWarsAPI.Entities;

public class BaseEntity<T>
{
    [Key]
    public T? Id { get; set; }
}