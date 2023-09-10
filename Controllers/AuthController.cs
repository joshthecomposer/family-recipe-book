using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.CreateAsync(user);
                return user;
            }
            catch
            {
                return StatusCode(500, "Error saving resource to the database, try again.");
            }
        }
        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login()
    {
        await Task.Delay(1);
        return Ok("Login");
    }
}
