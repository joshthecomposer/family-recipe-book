using Microsoft.AspNetCore.Mvc;
using MyApp.DTOs.RecipeDTOs;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly ITokenService _tokenService;

    public RecipeController(IRecipeService rs, ITokenService ts)
    {
        _recipeService = rs;
        _tokenService = ts;
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(RecipeNoUserIdDTO recipe)
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

        try
        {
            await _recipeService.CreateAsync(recipe, claim);
            return StatusCode(201, "Successfully created.");
        }
        catch
        {
            return StatusCode(500, "Error saving new recipe.");
        }
    }
}
