using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
string connectionString;
if (builder.Environment.IsProduction())
{
    connectionString = builder.Configuration.GetConnectionString("LocalConnection")!;
}
else
{
    connectionString = builder.Configuration.GetConnectionString("AwsConnection")!;
}

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
