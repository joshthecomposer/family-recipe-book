using MyApp.DataStorage;
using MyApp.Models;

namespace MyApp.Services;
public class RecipeService :IRecipeService
{
    private readonly DBContext _db;
    
    public RecipeService(DBContext db)
    {
        _db = db;
    }

    public async Task CreateAsync(Recipe recipe)
    {
        //TODO: error catching
        await _db.Recipes.AddAsync(recipe);
        await _db.SaveChangesAsync();
    }
}