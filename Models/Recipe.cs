#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;
using MyApp.DTOs.RecipeDTOs;

namespace MyApp.Models;
public class Recipe : BaseEntity
{
    [Key]
    public int RecipeId { get; set; }
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a brief description so others know what this recipe is.")]
    public string Description { get; set; }
    
    //Associations
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new();

    //TODO: 1 to 1 relationship preview photo for the recipe.
}