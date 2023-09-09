#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.DataStorage;
public class DBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DBContext(DbContextOptions options) : base(options) { }
}