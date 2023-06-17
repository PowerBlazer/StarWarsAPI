using Microsoft.EntityFrameworkCore;
using StarWarsAPI.Context.Abstraction;
using StarWarsAPI.Entities;

namespace StarWarsAPI.Context;

public sealed class DatabaseContext: DbContext,IDatabaseContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Character> Characters => Set<Character>();
    public DbSet<Movie> Movies => Set<Movie>();

    public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>()
            .HasMany(p => p.Movies)
            .WithOne(p => p.Character)
            .HasForeignKey(p => p.CharacterId);
        
        modelBuilder.Entity<User>()
            .HasMany(p => p.Characters)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.OwnerId);
        
        #region HasData
        modelBuilder.Entity<Character>()
            .HasData(
                new Character
                {
                    Id = 1,
                    Name = "Jhone",
                    BirthDate = DateTimeOffset.UtcNow,
                    HomeWorld = "Earth",
                    Gender = "Man",
                    Species = "Human",
                    Height = 170,
                    HairColor = "Black",
                    EyeColor = "Blue",
                    Description = "Very strong and beatiful"
                },
                new Character
                {
                    Id = 2,
                    Name = "Mikle",
                    BirthDate = DateTimeOffset.UtcNow,
                    HomeWorld = "Earth",
                    Gender = "Woman",
                    Species = "Human",
                    Height = 192,
                    HairColor = "Red",
                    EyeColor = "Green",
                    Description = "Very strong and beatiful"
                },
                new Character
                {
                    Id = 3,
                    Name = "Katya",
                    BirthDate = DateTimeOffset.UtcNow,
                    HomeWorld = "Mars",
                    Gender = "Woman",
                    Species = "Human",
                    Height = 192,
                    HairColor = "Blue",
                    EyeColor = "Green",
                    Description = "Very beatiful"
                });

        modelBuilder.Entity<Movie>()
            .HasData(
                new Movie
                {
                    Id = 1,
                    CharacterId = 1,
                    Title = "StarWars v1"
                },
                new Movie
                {
                    Id = 2,
                    CharacterId = 2,
                    Title = "StarWars v2"
                });

        #endregion
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}