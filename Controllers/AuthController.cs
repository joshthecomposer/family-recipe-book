using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
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
    public async Task<ActionResult<TokensDTO>> Login(LoginUser loginUser)
    {
        if (!ModelState.IsValid) 
        { 
            return BadRequest(ModelState); 
        }

        UserDTO? validUser = await _userService.ValidateUserPassword(loginUser);

        if (validUser == null) 
        { 
            return BadRequest("Invalid email or password"); 
        }

        bool refreshTokensCleared = await _tokenService.DeactivateTokensForUserAsync(validUser.UserId);

        if (refreshTokensCleared == false) 
        { 
            return StatusCode(500, "Error updating user tokens, try again."); 
        }

        TokensDTO? tokens = await _tokenService.CreateTokensDTO(validUser.UserId);

        if (tokens == null) 
        { 
            return StatusCode(500, "error saving new refreshtoken to database"); 
        }

        return Ok(tokens);
    }
}
