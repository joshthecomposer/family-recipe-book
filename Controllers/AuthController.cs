using Microsoft.AspNetCore.Mvc;
using MyApp.DataTransfer.Tokens;
using MyApp.DataTransfer.Users;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.CreateAsync(user);
                return StatusCode(201, "Successfully registered, please login.");
            }
            catch
            {
                return StatusCode(500, "Error saving resource to the database, try again.");
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokensDto>> Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        UserDto? validUser = await _userService.ValidateUserPassword(loginUser);

        if (validUser == null)
        {
            return BadRequest("Invalid email or password");
        }

        bool refreshTokensCleared = await _tokenService.DeactivateTokensForUserAsync(validUser.UserId);

        if (refreshTokensCleared == false)
        {
            return StatusCode(500, "Error updating user tokens, try again.");
        }

        TokensDto? tokens = await _tokenService.CreateTokensDTOAsync(validUser.UserId);

        if (tokens == null)
        {
            return StatusCode(500, "error saving new refreshtoken to database");
        }

        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokensDto>> DoRefreshActionAsync(RefreshRequestDTO rft)
    {
        int checkValue = await _tokenService.ValidateRefreshTokenAsync(rft.Rft);
        if (checkValue < 1)
        {
            return Unauthorized("Token not found.");
        }
        TokensDto? tokens = await _tokenService.CreateTokensDTOAsync(checkValue);
        if (tokens == null)
        {
            return StatusCode(500, "Something went wrong creating the TokensDTO");
        }
        return tokens;
    }
}
