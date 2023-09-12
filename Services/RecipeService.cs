using MyApp.DataStorage;
using MyApp.DTOs.RecipeDTOs;
using MyApp.Models;

namespace MyApp.Services;
public class RecipeService :IRecipeService
{
    private readonly DBContext _db;
    
    public RecipeService(DBContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(RecipeNoUserIdDTO recipe, int userId)
    {
        Recipe ingoing = new()
        {
            Name = recipe.Name,
            Description = recipe.Description,
            UserId = userId
        };
        await _db.Recipes.AddAsync(ingoing);
        await _db.SaveChangesAsync();
    }
}