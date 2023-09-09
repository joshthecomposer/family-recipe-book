#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;

namespace MyApp.DataStorage;
public class DBContext : DbContext
{
    public DBContext(DbContextOptions options) : base(options) { }
}