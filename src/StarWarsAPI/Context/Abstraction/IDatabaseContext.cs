using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StarWarsAPI.Entities;

namespace StarWarsAPI.Context.Abstraction;

public interface IDatabaseContext
{
    DbSet<User> Users { get; }
    DbSet<Character> Characters { get; }
    DbSet<Movie> Movies { get; }
    Task<int> SaveChangesAsync();
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}