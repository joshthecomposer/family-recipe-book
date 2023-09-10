using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using System.Security.Claims;
using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MyApp.Services;
public class TokenService : ITokenService
{
    private readonly DBContext _db;
    private readonly IConfiguration _config;

    public TokenService(DBContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    private string GenerateRefreshToken()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] byteToken = new byte[32]; //128 bit
        rng.GetBytes(byteToken);
        return Convert.ToBase64String(byteToken);
    }

    public string GenerateAccessToken(int id)
    {
        string? encKey = _config["AppSecrets:JWTSecret"];
        if (string.IsNullOrEmpty(encKey) || encKey.Length < 16)
        {
            throw new InvalidOperationException("Jwt Secret is Invalid or Missing.");
        }

        byte[] key = Encoding.ASCII.GetBytes(encKey);

        JwtSecurityTokenHandler handler = new();
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, Convert.ToString(id))
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public async Task<bool> DeactivateTokensForUserAsync(int id)
    {
        var tokens = await _db.RefreshTokens
                            .Where(t => t.UserId == id)
                            .ToListAsync();
        try
        {
            foreach (var t in tokens)
            {
                t.IsActive = false;
                t.DisabledAt = DateTime.UtcNow;
                t.UpdatedAt = DateTime.UtcNow;
            }
            await _db.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<TokensDTO?> CreateTokensDTO(int userId)
    {
        RefreshToken rft = new()
        {
            Value = GenerateRefreshToken(),
            UserId = userId
        };
        try
        {
            string jwt = GenerateAccessToken(userId);
            await _db.RefreshTokens.AddAsync(rft);
            await _db.SaveChangesAsync();
            return new TokensDTO(rft, jwt);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
        catch
        {
            Console.WriteLine("Unknown error in CreateTokensDTO");
            return null;
        }
    }
}