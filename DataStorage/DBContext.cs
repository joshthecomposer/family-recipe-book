#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.DataStorage;
public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DBContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasQueryFilter(u => u.IsActive);

        modelBuilder.Entity<User>(entity => {
        entity.HasIndex(e => e.Email).IsUnique();
        });
    }
}