using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.DataStorage;
using MyApp.DTOs.UserDTOs;
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
    public async Task<ActionResult> Login(LoginUser loginUser)
    {
        await Task.Delay(1);
        if (ModelState.IsValid)
        {

        }
        return BadRequest();
    }
}
