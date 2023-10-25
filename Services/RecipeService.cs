using MyApp.DataStorage;
using MyApp.DataTransfer.Recipes;
using MyApp.Models;

namespace MyApp.Services;
public class RecipeService :IRecipeService
{
    private readonly DBContext _db;

    public RecipeService(DBContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(RecipeNoUserId recipe, int userId)
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
