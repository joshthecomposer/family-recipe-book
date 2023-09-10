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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetByIdAsync(int id)
    {
        try
        {
            UserDTO? user = await _userService.GetByIdAsync(id);

            if (user == null) return NotFound("User not found.");

            return user;
        }
        catch
        {
            return StatusCode(500, "Error fetching user, please try again.");
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeactivateUserByIdAsync(int id)
    {
        try
        {
            User? user = await _userService.DeactivateUserByIdAsync(id);
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