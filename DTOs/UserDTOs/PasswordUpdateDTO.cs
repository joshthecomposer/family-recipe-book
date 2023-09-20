#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;
using MyApp.CustomAnnotations;

namespace MyApp.DTOs.UserDTOs;
public class PasswordUpdateDTO
{
    [Required]
    public string PrevPassword { get; set; }
    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }
    [Compare("NewPassword")]
    public string Confirm { get; set; }
}
