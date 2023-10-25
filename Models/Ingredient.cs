#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models;

public class Ingredient : BaseEntity
{
    [Key]
    public int IngredientId { get; set; }
    [Required]
    public string Value { get; set; }

    //Associations
    public int RecipeId { get; set; }
    public Recipe? Recipe { get; set; }
}
