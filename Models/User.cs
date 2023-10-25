#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyApp.CustomAnnotations;

namespace MyApp.Models;
public class User : BaseEntity
{
    [Key]
    public int UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    [UniqueEmail]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    [NotMapped]
    [Compare("Password")]
    public string Confirm { get; set; }

    public DateTime? DisabledAt { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
