using Microsoft.AspNetCore.Mvc;
using MyApp.DataStorage;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeactivateUserByIdAsync(int id)
    {
        try
        {
            User? user = await _userService.DeactivateUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }
        catch
        {
            // You might log the exception here
            return StatusCode(500, "Error deactivating user, please try again.");
        }
    }
}