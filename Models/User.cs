#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;
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
    [Compare("Password")]
    public string Confirm { get; set; }
}