using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;

namespace MyApp.Services;
public interface ITokenService
{
    Task<bool> DeactivateTokensForUserAsync(int userId);
    Task<TokensDTO?> CreateTokensDTOAsync(int userId);
    string GenerateAccessToken(int id);
    Task<int> ValidateAccessTokenAsync(string jwt);
    Task<int> ValidateRefreshTokenAsync(string rft);
    Task<TokensDTO> DoRefreshActionAsync(string rft);
}