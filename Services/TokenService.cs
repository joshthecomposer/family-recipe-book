using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using System.Security.Claims;
using MyApp.Models;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http.Headers;
using MyApp.DataTransfer.Tokens;

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
        string? encKey = _config["Jwt:SecretKey"];
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
            Audience = _config["Jwt:Audience"],
            Issuer = _config["Jwt:Issuer"],
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
                t.Expiry = DateTime.UtcNow.AddDays(-30);
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

    public async Task<TokensDto?> CreateTokensDTOAsync(int userId)
    {
        RefreshToken rft = new()
        {
            Value = GenerateRefreshToken(),
            UserId = userId
        };
        //TODO: Put this in a transaction.
        try
        {
            string jwt = GenerateAccessToken(userId);
            await DeactivateTokensForUserAsync(userId);
            await _db.RefreshTokens.AddAsync(rft);
            await _db.SaveChangesAsync();
            return new TokensDto(rft, jwt);
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

    public async Task<TokensDto> DoRefreshActionAsync(string rft)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    public int GetClaimFromHeaderValue(HttpRequest request)
    {
        AuthenticationHeaderValue? header = null;
        try
        {
            header = AuthenticationHeaderValue.Parse(request.Headers["Authorization"]);

        }
        catch (FormatException)
        {
            Console.WriteLine("Authorization header is malformed.");
            return -1;
        }
        catch (ArgumentNullException)
        {
            Console.WriteLine("Authorization header is missing.");
            return -1;
        }
        string? token = ExtractTokenFromHeaders(header);
        if (token == null)
        {
            return -1;
        }
        int? claim = GetClaimFromAccessToken(token);
        if (claim == null)
        {
            return -1;
        }
        return (int)claim;
    }

    static string? ExtractTokenFromHeaders(AuthenticationHeaderValue header)
    {
        if (string.IsNullOrEmpty(header.Parameter))
        {
            return null;
        }
        return header.Parameter;
    }

    static int? GetClaimFromAccessToken(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken? token = handler.ReadJwtToken(jwt);
        if (token == null)
        {
            Console.WriteLine("Handler failed to read token in GetClaimFromAccessToken");
            return null;
        }
        var claims = token.Claims;
        Claim? userIdClaim = claims.Where(c => c.Type == "unique_name").FirstOrDefault();

        if (userIdClaim == null)
        {
            Console.WriteLine("Failed to extract userIdClaim from token.");
            return null;
        }
        if (int.TryParse(userIdClaim.Value, out int id))
        {
            return id;
        }
        else
        {
            Console.WriteLine("Failed to parse int from claim.value");
            return null;
        }
    }

    public async Task<int> ValidateRefreshTokenAsync(string rft)
    {
        RefreshToken? refreshToken = await _db.RefreshTokens
                                        .Where(t => t.Value == rft && t.Expiry > DateTime.UtcNow)
                                        .SingleOrDefaultAsync();
        if (refreshToken == null)
        {
            return -1;
        }
        return refreshToken.UserId;
    }

    public async Task<InviteToken> CreateInviteCode(string email)
    {
        InviteToken invite = new()
        {
            Email = email,
            Value = GenerateRefreshToken()
        };
        await _db.InviteTokens.AddAsync(invite);
        await _db.SaveChangesAsync();
        return invite;
    }

}
