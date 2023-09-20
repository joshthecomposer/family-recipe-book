#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models;
public class InviteToken : BaseEntity
{
    [Key]
    public int InviteTokenId { get; set; }
    [Required]
    public string Value { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public DateTime Expiry { get; set; } = DateTime.UtcNow.AddHours(24);
}
