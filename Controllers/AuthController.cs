using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpGet("test")]
    public async Task<ActionResult> Test()
    {
        await Task.Delay(1);
        return Ok("Hello, world!");
    }
}
