using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;

namespace MyApp.Services;
public interface ITokenService
{
    Task<bool> DeactivateTokensForUserAsync(int userId);
    Task<TokensDTO?> CreateTokensDTO(int userId);
    string GenerateAccessToken(int id);
}