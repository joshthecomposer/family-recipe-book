#pragma warning disable 8618
using MyApp.Models;

namespace MyApp.DTOs.TokenDTOs;
public class TokensDTO
{
    public string RefreshToken { get; set; }
    public string AccessToken { get; set; }

    public TokensDTO(RefreshToken rft, string jwt)
    {
        RefreshToken = rft.Value;
        AccessToken = jwt;
    }
}