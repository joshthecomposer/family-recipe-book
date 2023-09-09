using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult> Register()
    {
        await Task.Delay(1);
        return Ok("Register");
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login()
    {
        await Task.Delay(1);
        return Ok("Login");
    }
}
