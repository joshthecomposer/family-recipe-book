using Microsoft.AspNetCore.Mvc;
using MyApp.DataStorage;
using MyApp.Models;

namespace MyApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly DBContext _db;

    public AuthController(DBContext db)
    {
        _db = db;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return user;
            }
            catch
            {
                return StatusCode(500, "Error saving resource to the database, try again.");
            }
        }
        return BadRequest(ModelState);
    }

    [HttpGet("test/{id}")]
    public async Task<ActionResult<User>> TestGetById(int id)
    {
        User? check = await _db.Users.FindAsync(id);
        if (check != null)
        {
            return check;
        }
        return NotFound("User not found.");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login()
    {
        await Task.Delay(1);
        return Ok("Login");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> DeactivateUserByIdAsync(int id)
    {
        User? check = await _db.Users.FindAsync(id);
        if (check != null)
        {
            try
            {
                check.IsActive = false;
                await _db.SaveChangesAsync();
                return check;
            }
            catch
            {
                return StatusCode(500, "Error deactivating user, please try again.");
            }
        }
        return NotFound("User not found.");
    }
}
