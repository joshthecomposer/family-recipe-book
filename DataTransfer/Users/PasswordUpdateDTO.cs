#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;

namespace MyApp.DataTransfer.Users;
public class PasswordUpdateDto
{
    [Required]
    public string PrevPassword { get; set; }
    [Required]
    [MinLength(8)]
    public string NewPassword { get; set; }
    [Compare("NewPassword")]
    public string Confirm { get; set; }
}
