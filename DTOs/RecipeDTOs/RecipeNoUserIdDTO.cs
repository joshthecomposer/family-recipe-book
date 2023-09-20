#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;

namespace MyApp.DTOs.RecipeDTOs;
public class RecipeNoUserIdDTO
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter a brief description so others know what this recipe is.")]
    public string Description { get; set; }
}
