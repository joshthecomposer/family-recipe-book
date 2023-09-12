using System.Net.Http.Headers;
using MyApp.DTOs.RecipeDTOs;
using MyApp.DTOs.TokenDTOs;
using MyApp.DTOs.UserDTOs;
using MyApp.Models;

namespace MyApp.Services;
public interface IRecipeService
{
    Task CreateAsync(RecipeNoUserIdDTO recipe, int userId);
}