#pragma warning disable 8618
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models;
public class RefreshToken : BaseEntity
{
    [Key]
    public int RefreshTokenId { get; set; }
    public string Value { get; set; }
    public DateTime Expiry { get; set; } = DateTime.UtcNow.AddDays(30);

    //Associations
    public int UserId { get; set; }
    public User User { get; set; }
}