#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;
using MyApp.CustomAnnotations;

namespace MyApp.DTOs.UserDTOs;
public class LoginUser
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
