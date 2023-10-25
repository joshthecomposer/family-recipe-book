using MyApp.DataTransfer.Recipes;

namespace MyApp.Services;
public interface IRecipeService
{
    Task CreateAsync(RecipeNoUserId recipe, int userId);
}
