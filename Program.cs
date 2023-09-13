using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyApp.DataStorage;
using MyApp.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddControllers();
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
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddDbContext<DBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

string? encKey = builder.Configuration["Jwt:SecretKey"];
if (string.IsNullOrEmpty(encKey) || encKey.Length < 16)
{
    throw new InvalidOperationException("Jwt Secret is Invalid or Missing.");
}

byte[] key = Encoding.ASCII.GetBytes(encKey);

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer("Bearer", options =>
	{
        //TODO: reenable this
		options.RequireHttpsMetadata = true;
		options.SaveToken = false;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
			ClockSkew = TimeSpan.Zero
		};
	});

builder.Services.AddControllersWithViews();
builder.Services.AddCors();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowOrigins");

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
	name: "/",
	pattern: "{*url}",
	defaults: new { controller = "Public", action = "Index" }
);

app.Run();
