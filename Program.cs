using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using MyApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
string connectionString;
if (builder.Environment.IsProduction())
{
    connectionString = builder.Configuration.GetConnectionString("AwsConnection")!;
}
else
{
    connectionString = builder.Configuration.GetConnectionString("LocalConnection")!;
}

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
