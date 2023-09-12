#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.DataStorage;
public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    public DBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);

        modelBuilder.Entity<User>(entity => {
        entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<RefreshToken>().HasQueryFilter(u => u.IsActive);

        modelBuilder.Entity<RefreshToken>(entity => {
        entity.HasIndex(e => e.Value).IsUnique();
        });

        modelBuilder.Entity<Recipe>().HasQueryFilter(u => u.IsActive);
        modelBuilder.Entity<Ingredient>().HasQueryFilter(u => u.IsActive);
    }
}