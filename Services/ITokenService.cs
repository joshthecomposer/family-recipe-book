using MyApp.DataTransfer.Tokens;

namespace MyApp.Services;
public interface ITokenService
{
    Task<bool> DeactivateTokensForUserAsync(int userId);
    Task<TokensDto?> CreateTokensDTOAsync(int userId);
    string GenerateAccessToken(int id);
    Task<int> ValidateRefreshTokenAsync(string rft);
    Task<TokensDto> DoRefreshActionAsync(string rft);
    int GetClaimFromHeaderValue(HttpRequest request);
}
