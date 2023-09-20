using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    [HttpGet]
    public async Task<ActionResult<UserDTO>> GetByIdAsync()
    {
        int claim = _tokenService.GetClaimFromHeaderValue(Request);
        if (claim < 1)
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

    [HttpPatch("update_password")]
    public async Task<ActionResult<UserDTO?>> UpdatePasswordAsync(PasswordUpdateDTO passUpdate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int claim = _tokenService.GetClaimFromHeaderValue(Request);
        if (claim < 1)
        {
            return NotFound("Invalid Claim.");
        }
        UserDTO? updatedUser = null;
        try
        {
            updatedUser = await _userService.UpdatePasswordAsync(passUpdate, claim);
            return updatedUser;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeactivateUserByIdAsync()
    {
        //TODO: Require password for deactivation.
        int claim = _tokenService.GetClaimFromHeaderValue(Request);
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
