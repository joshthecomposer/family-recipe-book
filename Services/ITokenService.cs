using System.Net.Http.Headers;
using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;

namespace MyApp.Services;
public interface ITokenService
{
    Task<bool> DeactivateTokensForUserAsync(int userId);
    Task<TokensDTO?> CreateTokensDTOAsync(int userId);
    string GenerateAccessToken(int id);
    Task<int> ValidateRefreshTokenAsync(string rft);
    Task<TokensDTO> DoRefreshActionAsync(string rft);
    int GetClaimFromHeaderValue(HttpRequest request);
}
