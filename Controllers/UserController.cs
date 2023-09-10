using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.DataStorage;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
    {
        var claim = _tokenService.GetClaimFromHeaderValue(Request);
        if (claim < 1 || claim !=id)
        {
            return NotFound("Invalid Claim.");
        }
        try
        {
            UserDTO? user = await _userService.GetByIdAsync(claim);

            if (user == null) return NotFound("User not found.");

            return user;
        }
        catch
        {
            return StatusCode(500, "Error fetching user, please try again.");
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeactivateUserByIdAsync()
    {
        var claim = _tokenService.GetClaimFromHeaderValue(Request);
        if (claim < 1)
        {
            return NotFound("Invalid Claim.");
        }
        try
        {
            User? user = await _userService.DeactivateUserByIdAsync(claim);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok();
        }
        catch
        {
            return StatusCode(500, "Error deactivating user, please try again.");
        }
    }
}